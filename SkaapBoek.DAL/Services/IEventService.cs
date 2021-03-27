using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IEventService
    {
        Task<IEnumerable<TaskInstance>> GetTasksDueToday();
        Task<IEnumerable<TaskInstance>> GetOverdueTasks();
        Task<IEnumerable<TaskInstance>> GetUpcomingTasks(int days);
        Task<IEnumerable<MilsEventModel>> GetTodaysMilsEvents();
        Task<IEnumerable<MilsEventModel>> GetOverdueMilsEvents();
        Task<IEnumerable<MilsEventModel>> GetUpcomingMilsEvents(int days);
        Task<MilsEventModel> GetMilsEventByGroupId(int groupId);
    }

    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly DateTime _today = DateTime.Today;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskInstance>> GetTasksDueToday()
        {
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Where(t => t.Status.Name != "Completed" 
                        && ((t.StartDate.Date == DateTime.Today && t.StatusId != 2)
                            || (t.EndDate ?? DateTime.MinValue) == DateTime.Today))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskInstance>> GetOverdueTasks()
        {
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Where(t => (t.StartDate < DateTime.Today 
                        && (t.EndDate < DateTime.Today || t.EndDate == null)
                        && t.StatusId != 3)
                    || (t.StartDate < DateTime.Today 
                        && t.EndDate > DateTime.Today
                        && t.StatusId == 1))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskInstance>> GetUpcomingTasks(int days)
        {
            var today = DateTime.Today;
            var toDate = DateTime.Today.AddDays(days);
            var tomorrow = today.AddDays(1);
            var incompleteIds = new[] { 1, 2 };
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .AsNoTracking()
                .Where(t => (t.StartDate > tomorrow && t.StartDate <= toDate && t.StatusId == 1)
                    || (t.EndDate >= tomorrow && t.EndDate <= toDate && t.StatusId == 2))
                .ToListAsync();
        }

        public async Task<IEnumerable<MilsEventModel>> GetUpcomingMilsEvents(int days)
        {
            var toDate = _today.AddDays(days);
            var groups = _context.GroupSet
                .Include(g => g.MilsPhase.Pen)
                .Include(g => g.MilsPhase.Tasks)
                .Where(g => g.MilsPhaseId != null)
                .AsNoTracking();
            return await PopulateMilsEventCollection(groups);
        }

        public async Task<IEnumerable<MilsEventModel>> GetOverdueMilsEvents()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var groups = _context.GroupSet
               .Include(g => g.MilsPhase.Pen)
               .Include(g => g.MilsPhase.Tasks)
               .AsNoTracking()
               .Where(g => g.PhaseStartDate != null 
                   && g.PhaseEndDate != null 
                   && g.MilsPhaseId != null
                        && (g.PhaseEndDate < today));

            return await PopulateMilsEventCollection(groups);
        }

        public async Task<IEnumerable<MilsEventModel>> GetTodaysMilsEvents()
        {
            var groups = _context.GroupSet
                .Include(g => g.MilsPhase.Pen)
                .Include(g => g.MilsPhase.Tasks)
                .AsNoTracking()
                .Where(g => g.PhaseStartDate != null && g.MilsPhaseId != null
                    && (g.PhaseStartDate == _today || g.PhaseEndDate == _today));

            var result = await PopulateMilsEventCollection(groups);
            return result;
        }

        public async Task<MilsEventModel> GetMilsEventByGroupId(int groupId)
        {
            var group = await _context.GroupSet
                .Include(g => g.MilsPhase.Tasks)
                .Include(g => g.Pen)
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == groupId);

            if (group is null || group.MilsPhaseId is null)
                return new MilsEventModel();

            var eventModel = new MilsEventModel
            {
                GroupId = group.Id,
                GroupName = group.Name,
                PenName = group.Pen.Name,
                StartDate = group.PhaseStartDate.Value,
                EndDate = group.PhaseEndDate.Value,
                CurrentPhaseActivity = group.MilsPhase.Activity,
                CurrentPhaseSequence = group.MilsPhase.PhaseSequence,
                CurrentPhaseId = group.MilsPhaseId.Value,
            };

            var instructions = new List<string>();
            foreach(var instruction in group.MilsPhase.Tasks)
            {
                instructions.Add(instruction.Instructions);
            }

            eventModel.Instructions = instructions;
            return eventModel;
        }

        //public async Task<>

        private async Task<IEnumerable<MilsEventModel>> PopulateMilsEventCollection(IQueryable<Group> groups)
        {
            var maxSequence = await _context.MilsPhaseSet.MaxAsync(p => p.PhaseSequence);
            var upcomingEvents = await groups.Select(g => new MilsEventModel
            {
                GroupId = g.Id,
                GroupName = g.Name,
                PenName = g.Pen.Name,
                StartDate = g.PhaseStartDate.Value,
                EndDate = g.PhaseEndDate.Value,
                CurrentPhaseActivity = g.MilsPhase.Activity,
                CurrentPhaseSequence = g.MilsPhase.PhaseSequence,
                CurrentPhaseId = g.MilsPhaseId.Value,
            }).ToListAsync();

            var phaseDict = await _context.MilsPhaseSet
                .AsNoTracking()
                .Select(p => new
                {
                    p.PhaseSequence,
                    p.Id,
                    p.Activity
                })
                .ToDictionaryAsync(p => p.PhaseSequence);

            foreach (var milsEvent in upcomingEvents)
            {
                if (milsEvent.CurrentPhaseSequence != maxSequence)
                {
                    milsEvent.NextPhaseId = phaseDict[milsEvent.CurrentPhaseSequence + 1].Id;
                    milsEvent.NextPhaseActivity = phaseDict[milsEvent.CurrentPhaseSequence + 1].Activity;
                }
            }

            return upcomingEvents;
        }
    }
}
