/* global require, module */

var Angular2App = require('angular-cli/lib/broccoli/angular2-app');

module.exports = function(defaults) {
  return new Angular2App(defaults, {
    vendorNpmFiles: [
      'systemjs/dist/system-polyfills.js',
      'systemjs/dist/system.src.js',
      'zone.js/dist/**/*.+(js|js.map)',
      'es6-shim/es6-shim.js',
      'reflect-metadata/**/*.+(ts|js|js.map)',
      'rxjs/**/*.+(js|js.map)',
      '@angular/**/*.+(js|js.map)',
      'angular2-google-maps/**/*.+(js|js.map)',
      'ng2-bootstrap/**/*.+(js|js.map)',
      'moment/moment.js',
      'suncalc/suncalc.js'
    ]
    // ,polyfills:[
    //   "libraries/suncalc.js",
    //   "libraries/moment.js",
    //   "libraries/bootstrap.min.js",
    //   "libraries/bootstrap-datetimepicker.min.js"
    // ]
  });
};
