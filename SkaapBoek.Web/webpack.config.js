const path = require('path');

module.exports = {
    // dev mode and source mapping
    mode: 'development',
    devtool: 'inline-source-map',

    // define entry points
    entry: {
        main: './assets/main.js',
        dependencies: './assets/dependencies.js'
    },

    // define output files
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname + '/wwwroot', 'dist'),
    },

    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
                test: /\.s[ac]ss$/i,
                exclude: /(node_modules)/,
                use: [
                    // Creates `style` nodes from JS strings
                    'style-loader',
                    // Translates CSS into CommonJS
                    'css-loader',
                    // Compiles Sass to CSS
                    'sass-loader',
                ],
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            }
        ]
    }
}