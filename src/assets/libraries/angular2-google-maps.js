System.registerDynamic("angular2-google-maps/directives/google-map.js", ["@angular/core", "../services/google-maps-api-wrapper", "../services/marker-manager", "../services/info-window-manager"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var google_maps_api_wrapper_1 = $__require('../services/google-maps-api-wrapper');
  var marker_manager_1 = $__require('../services/marker-manager');
  var info_window_manager_1 = $__require('../services/info-window-manager');
  var SebmGoogleMap = (function() {
    function SebmGoogleMap(_elem, _mapsWrapper) {
      this._elem = _elem;
      this._mapsWrapper = _mapsWrapper;
      this._longitude = 0;
      this._latitude = 0;
      this._zoom = 8;
      this.disableDoubleClickZoom = false;
      this.disableDefaultUI = false;
      this.scrollwheel = true;
      this.keyboardShortcuts = true;
      this.zoomControl = true;
      this.mapClick = new core_1.EventEmitter();
      this.mapRightClick = new core_1.EventEmitter();
      this.mapDblClick = new core_1.EventEmitter();
      this.centerChange = new core_1.EventEmitter();
    }
    SebmGoogleMap.prototype.ngOnInit = function() {
      var container = this._elem.nativeElement.querySelector('.sebm-google-map-container-inner');
      this._initMapInstance(container);
    };
    SebmGoogleMap.prototype._initMapInstance = function(el) {
      this._mapsWrapper.createMap(el, {
        center: {
          lat: this._latitude,
          lng: this._longitude
        },
        zoom: this._zoom,
        disableDefaultUI: this.disableDefaultUI,
        backgroundColor: this.backgroundColor,
        draggableCursor: this.draggableCursor,
        draggingCursor: this.draggingCursor,
        keyboardShortcuts: this.keyboardShortcuts,
        zoomControl: this.zoomControl
      });
      this._handleMapCenterChange();
      this._handleMapZoomChange();
      this._handleMapMouseEvents();
    };
    SebmGoogleMap.prototype.ngOnChanges = function(changes) {
      this._updateMapOptionsChanges(changes);
    };
    SebmGoogleMap.prototype._updateMapOptionsChanges = function(changes) {
      var options = {};
      var optionKeys = Object.keys(changes).filter(function(k) {
        return SebmGoogleMap._mapOptionsAttributes.indexOf(k) !== -1;
      });
      optionKeys.forEach(function(k) {
        options[k] = changes[k].currentValue;
      });
      this._mapsWrapper.setMapOptions(options);
    };
    SebmGoogleMap.prototype.triggerResize = function() {
      var _this = this;
      return new Promise(function(resolve) {
        setTimeout(function() {
          return _this._mapsWrapper.triggerMapEvent('resize').then(function() {
            return resolve();
          });
        });
      });
    };
    Object.defineProperty(SebmGoogleMap.prototype, "zoom", {
      set: function(value) {
        this._zoom = this._convertToDecimal(value, 8);
        if (typeof this._zoom === 'number') {
          this._mapsWrapper.setZoom(this._zoom);
        }
      },
      enumerable: true,
      configurable: true
    });
    Object.defineProperty(SebmGoogleMap.prototype, "longitude", {
      set: function(value) {
        this._longitude = this._convertToDecimal(value);
        this._updateCenter();
      },
      enumerable: true,
      configurable: true
    });
    Object.defineProperty(SebmGoogleMap.prototype, "latitude", {
      set: function(value) {
        this._latitude = this._convertToDecimal(value);
        this._updateCenter();
      },
      enumerable: true,
      configurable: true
    });
    SebmGoogleMap.prototype._convertToDecimal = function(value, defaultValue) {
      if (defaultValue === void 0) {
        defaultValue = null;
      }
      if (typeof value === 'string') {
        return parseFloat(value);
      } else if (typeof value === 'number') {
        return value;
      }
      return defaultValue;
    };
    SebmGoogleMap.prototype._updateCenter = function() {
      if (typeof this._latitude !== 'number' || typeof this._longitude !== 'number') {
        return;
      }
      this._mapsWrapper.setCenter({
        lat: this._latitude,
        lng: this._longitude
      });
    };
    SebmGoogleMap.prototype._handleMapCenterChange = function() {
      var _this = this;
      this._mapsWrapper.subscribeToMapEvent('center_changed').subscribe(function() {
        _this._mapsWrapper.getCenter().then(function(center) {
          _this._latitude = center.lat();
          _this._longitude = center.lng();
          _this.centerChange.emit({
            lat: _this._latitude,
            lng: _this._longitude
          });
        });
      });
    };
    SebmGoogleMap.prototype._handleMapZoomChange = function() {
      var _this = this;
      this._mapsWrapper.subscribeToMapEvent('zoom_changed').subscribe(function() {
        _this._mapsWrapper.getZoom().then(function(z) {
          return _this._zoom = z;
        });
      });
    };
    SebmGoogleMap.prototype._handleMapMouseEvents = function() {
      var _this = this;
      var events = [{
        name: 'click',
        emitter: this.mapClick
      }, {
        name: 'rightclick',
        emitter: this.mapRightClick
      }];
      events.forEach(function(e) {
        _this._mapsWrapper.subscribeToMapEvent(e.name).subscribe(function(event) {
          var value = {coords: {
              lat: event.latLng.lat(),
              lng: event.latLng.lng()
            }};
          e.emitter.emit(value);
        });
      });
    };
    SebmGoogleMap._mapOptionsAttributes = ['disableDoubleClickZoom', 'scrollwheel', 'draggableCursor', 'draggingCursor', 'keyboardShortcuts', 'zoomControl'];
    SebmGoogleMap = __decorate([core_1.Component({
      selector: 'sebm-google-map',
      providers: [google_maps_api_wrapper_1.GoogleMapsAPIWrapper, marker_manager_1.MarkerManager, info_window_manager_1.InfoWindowManager],
      inputs: ['longitude', 'latitude', 'zoom', 'disableDoubleClickZoom', 'disableDefaultUI', 'scrollwheel', 'backgroundColor', 'draggableCursor', 'draggingCursor', 'keyboardShortcuts', 'zoomControl'],
      outputs: ['mapClick', 'mapRightClick', 'mapDblClick', 'centerChange'],
      host: {'[class.sebm-google-map-container]': 'true'},
      styles: ["\n    .sebm-google-map-container-inner {\n      width: inherit;\n      height: inherit;\n    }\n    .sebm-google-map-content {\n      display:none;\n    }\n  "],
      template: "\n    <div class='sebm-google-map-container-inner'></div>\n    <div class='sebm-google-map-content'>\n      <ng-content></ng-content>\n    </div>\n  "
    }), __metadata('design:paramtypes', [core_1.ElementRef, google_maps_api_wrapper_1.GoogleMapsAPIWrapper])], SebmGoogleMap);
    return SebmGoogleMap;
  }());
  exports.SebmGoogleMap = SebmGoogleMap;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/directives/google-map-marker.js", ["@angular/core", "../services/marker-manager", "./google-map-info-window"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var marker_manager_1 = $__require('../services/marker-manager');
  var google_map_info_window_1 = $__require('./google-map-info-window');
  var markerId = 0;
  var SebmGoogleMapMarker = (function() {
    function SebmGoogleMapMarker(_markerManager) {
      this._markerManager = _markerManager;
      this.draggable = false;
      this.markerClick = new core_1.EventEmitter();
      this.dragEnd = new core_1.EventEmitter();
      this._markerAddedToManger = false;
      this._id = (markerId++).toString();
    }
    SebmGoogleMapMarker.prototype.ngAfterContentInit = function() {
      if (this._infoWindow != null) {
        this._infoWindow.hostMarker = this;
      }
    };
    SebmGoogleMapMarker.prototype.ngOnChanges = function(changes) {
      if (typeof this.latitude !== 'number' || typeof this.longitude !== 'number') {
        return;
      }
      if (!this._markerAddedToManger) {
        this._markerManager.addMarker(this);
        this._markerAddedToManger = true;
        this._addEventListeners();
        return;
      }
      if (changes['latitude'] || changes['longitude']) {
        this._markerManager.updateMarkerPosition(this);
      }
      if (changes['title']) {
        this._markerManager.updateTitle(this);
      }
      if (changes['label']) {
        this._markerManager.updateLabel(this);
      }
      if (changes['draggable']) {
        this._markerManager.updateDraggable(this);
      }
      if (changes['iconUrl']) {
        this._markerManager.updateIcon(this);
      }
    };
    SebmGoogleMapMarker.prototype._addEventListeners = function() {
      var _this = this;
      this._markerManager.createEventObservable('click', this).subscribe(function() {
        if (_this._infoWindow != null) {
          _this._infoWindow.open();
        }
        _this.markerClick.next(null);
      });
      this._markerManager.createEventObservable('dragend', this).subscribe(function(e) {
        _this.dragEnd.next({coords: {
            lat: e.latLng.lat(),
            lng: e.latLng.lng()
          }});
      });
    };
    SebmGoogleMapMarker.prototype.id = function() {
      return this._id;
    };
    SebmGoogleMapMarker.prototype.toString = function() {
      return 'SebmGoogleMapMarker-' + this._id.toString();
    };
    SebmGoogleMapMarker.prototype.ngOnDestroy = function() {
      this._markerManager.deleteMarker(this);
    };
    __decorate([core_1.ContentChild(google_map_info_window_1.SebmGoogleMapInfoWindow), __metadata('design:type', google_map_info_window_1.SebmGoogleMapInfoWindow)], SebmGoogleMapMarker.prototype, "_infoWindow", void 0);
    SebmGoogleMapMarker = __decorate([core_1.Directive({
      selector: 'sebm-google-map-marker',
      inputs: ['latitude', 'longitude', 'title', 'label', 'draggable: markerDraggable', 'iconUrl'],
      outputs: ['markerClick', 'dragEnd']
    }), __metadata('design:paramtypes', [marker_manager_1.MarkerManager])], SebmGoogleMapMarker);
    return SebmGoogleMapMarker;
  }());
  exports.SebmGoogleMapMarker = SebmGoogleMapMarker;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/directives/google-map-info-window.js", ["@angular/core", "../services/info-window-manager"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var info_window_manager_1 = $__require('../services/info-window-manager');
  var infoWindowId = 0;
  var SebmGoogleMapInfoWindow = (function() {
    function SebmGoogleMapInfoWindow(_infoWindowManager, _el) {
      this._infoWindowManager = _infoWindowManager;
      this._el = _el;
      this._infoWindowAddedToManager = false;
      this._id = (infoWindowId++).toString();
    }
    SebmGoogleMapInfoWindow.prototype.ngOnInit = function() {
      this.content = this._el.nativeElement.querySelector('.sebm-google-map-info-window-content');
      this._infoWindowManager.addInfoWindow(this);
      this._infoWindowAddedToManager = true;
    };
    SebmGoogleMapInfoWindow.prototype.ngOnChanges = function(changes) {
      if (!this._infoWindowAddedToManager) {
        return;
      }
      if ((changes['latitude'] || changes['longitude']) && typeof this.latitude === 'number' && typeof this.longitude === 'number') {
        this._infoWindowManager.setPosition(this);
      }
      if (changes['zIndex']) {
        this._infoWindowManager.setZIndex(this);
      }
      this._setInfoWindowOptions(changes);
    };
    SebmGoogleMapInfoWindow.prototype._setInfoWindowOptions = function(changes) {
      var options = {};
      var optionKeys = Object.keys(changes).filter(function(k) {
        return SebmGoogleMapInfoWindow._infoWindowOptionsInputs.indexOf(k) !== -1;
      });
      optionKeys.forEach(function(k) {
        options[k] = changes[k].currentValue;
      });
      this._infoWindowManager.setOptions(this, options);
    };
    SebmGoogleMapInfoWindow.prototype.open = function() {
      return this._infoWindowManager.open(this);
    };
    SebmGoogleMapInfoWindow.prototype.close = function() {
      return this._infoWindowManager.close(this);
    };
    SebmGoogleMapInfoWindow.prototype.id = function() {
      return this._id;
    };
    SebmGoogleMapInfoWindow.prototype.toString = function() {
      return 'SebmGoogleMapInfoWindow-' + this._id.toString();
    };
    SebmGoogleMapInfoWindow.prototype.ngOnDestroy = function() {
      this._infoWindowManager.deleteInfoWindow(this);
    };
    SebmGoogleMapInfoWindow._infoWindowOptionsInputs = ['disableAutoPan', 'maxWidth'];
    SebmGoogleMapInfoWindow = __decorate([core_1.Component({
      selector: 'sebm-google-map-info-window',
      inputs: ['latitude', 'longitude', 'disableAutoPan'],
      template: "<div class='sebm-google-map-info-window-content'>\n      <ng-content></ng-content>\n    </div>\n  "
    }), __metadata('design:paramtypes', [info_window_manager_1.InfoWindowManager, core_1.ElementRef])], SebmGoogleMapInfoWindow);
    return SebmGoogleMapInfoWindow;
  }());
  exports.SebmGoogleMapInfoWindow = SebmGoogleMapInfoWindow;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/directives-const.js", ["./directives/google-map", "./directives/google-map-marker", "./directives/google-map-info-window"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var google_map_1 = $__require('./directives/google-map');
  var google_map_marker_1 = $__require('./directives/google-map-marker');
  var google_map_info_window_1 = $__require('./directives/google-map-info-window');
  exports.ANGULAR2_GOOGLE_MAPS_DIRECTIVES = [google_map_1.SebmGoogleMap, google_map_marker_1.SebmGoogleMapMarker, google_map_info_window_1.SebmGoogleMapInfoWindow];
  return module.exports;
});

System.registerDynamic("angular2-google-maps/directives.js", ["./directives/google-map", "./directives/google-map-marker", "./directives/google-map-info-window", "./directives-const"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var google_map_1 = $__require('./directives/google-map');
  exports.SebmGoogleMap = google_map_1.SebmGoogleMap;
  var google_map_marker_1 = $__require('./directives/google-map-marker');
  exports.SebmGoogleMapMarker = google_map_marker_1.SebmGoogleMapMarker;
  var google_map_info_window_1 = $__require('./directives/google-map-info-window');
  exports.SebmGoogleMapInfoWindow = google_map_info_window_1.SebmGoogleMapInfoWindow;
  var directives_const_1 = $__require('./directives-const');
  exports.ANGULAR2_GOOGLE_MAPS_DIRECTIVES = directives_const_1.ANGULAR2_GOOGLE_MAPS_DIRECTIVES;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/maps-api-loader/noop-maps-api-loader.js", [], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var NoOpMapsAPILoader = (function() {
    function NoOpMapsAPILoader() {}
    NoOpMapsAPILoader.prototype.load = function() {
      if (!window.google || !window.google.maps) {
        throw new Error('Google Maps API not loaded on page. Make sure window.google.maps is available!');
      }
      return Promise.resolve();
    };
    ;
    return NoOpMapsAPILoader;
  }());
  exports.NoOpMapsAPILoader = NoOpMapsAPILoader;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/google-maps-api-wrapper.js", ["@angular/core", "rxjs/Observable", "./maps-api-loader/maps-api-loader"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var Observable_1 = $__require('rxjs/Observable');
  var maps_api_loader_1 = $__require('./maps-api-loader/maps-api-loader');
  var GoogleMapsAPIWrapper = (function() {
    function GoogleMapsAPIWrapper(_loader, _zone) {
      var _this = this;
      this._loader = _loader;
      this._zone = _zone;
      this._map = new Promise(function(resolve) {
        _this._mapResolver = resolve;
      });
    }
    GoogleMapsAPIWrapper.prototype.createMap = function(el, mapOptions) {
      var _this = this;
      return this._loader.load().then(function() {
        var map = new google.maps.Map(el, mapOptions);
        _this._mapResolver(map);
        return;
      });
    };
    GoogleMapsAPIWrapper.prototype.setMapOptions = function(options) {
      this._map.then(function(m) {
        m.setOptions(options);
      });
    };
    GoogleMapsAPIWrapper.prototype.createMarker = function(options) {
      if (options === void 0) {
        options = {};
      }
      return this._map.then(function(map) {
        options.map = map;
        return new google.maps.Marker(options);
      });
    };
    GoogleMapsAPIWrapper.prototype.createInfoWindow = function(options) {
      return this._map.then(function() {
        return new google.maps.InfoWindow(options);
      });
    };
    GoogleMapsAPIWrapper.prototype.subscribeToMapEvent = function(eventName) {
      var _this = this;
      return Observable_1.Observable.create(function(observer) {
        _this._map.then(function(m) {
          m.addListener(eventName, function(arg) {
            _this._zone.run(function() {
              return observer.next(arg);
            });
          });
        });
      });
    };
    GoogleMapsAPIWrapper.prototype.setCenter = function(latLng) {
      return this._map.then(function(map) {
        return map.setCenter(latLng);
      });
    };
    GoogleMapsAPIWrapper.prototype.getZoom = function() {
      return this._map.then(function(map) {
        return map.getZoom();
      });
    };
    GoogleMapsAPIWrapper.prototype.setZoom = function(zoom) {
      return this._map.then(function(map) {
        return map.setZoom(zoom);
      });
    };
    GoogleMapsAPIWrapper.prototype.getCenter = function() {
      return this._map.then(function(map) {
        return map.getCenter();
      });
    };
    GoogleMapsAPIWrapper.prototype.getMap = function() {
      return this._map;
    };
    GoogleMapsAPIWrapper.prototype.triggerMapEvent = function(eventName) {
      return this._map.then(function(m) {
        return google.maps.event.trigger(m, eventName);
      });
    };
    GoogleMapsAPIWrapper = __decorate([core_1.Injectable(), __metadata('design:paramtypes', [maps_api_loader_1.MapsAPILoader, core_1.NgZone])], GoogleMapsAPIWrapper);
    return GoogleMapsAPIWrapper;
  }());
  exports.GoogleMapsAPIWrapper = GoogleMapsAPIWrapper;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/marker-manager.js", ["@angular/core", "rxjs/Observable", "./google-maps-api-wrapper"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var Observable_1 = $__require('rxjs/Observable');
  var google_maps_api_wrapper_1 = $__require('./google-maps-api-wrapper');
  var MarkerManager = (function() {
    function MarkerManager(_mapsWrapper, _zone) {
      this._mapsWrapper = _mapsWrapper;
      this._zone = _zone;
      this._markers = new Map();
    }
    MarkerManager.prototype.deleteMarker = function(marker) {
      var _this = this;
      var m = this._markers.get(marker);
      if (m == null) {
        return Promise.resolve();
      }
      return m.then(function(m) {
        return _this._zone.run(function() {
          m.setMap(null);
          _this._markers.delete(marker);
        });
      });
    };
    MarkerManager.prototype.updateMarkerPosition = function(marker) {
      return this._markers.get(marker).then(function(m) {
        return m.setPosition({
          lat: marker.latitude,
          lng: marker.longitude
        });
      });
    };
    MarkerManager.prototype.updateTitle = function(marker) {
      return this._markers.get(marker).then(function(m) {
        return m.setTitle(marker.title);
      });
    };
    MarkerManager.prototype.updateLabel = function(marker) {
      return this._markers.get(marker).then(function(m) {
        m.setLabel(marker.label);
      });
    };
    MarkerManager.prototype.updateDraggable = function(marker) {
      return this._markers.get(marker).then(function(m) {
        return m.setDraggable(marker.draggable);
      });
    };
    MarkerManager.prototype.updateIcon = function(marker) {
      return this._markers.get(marker).then(function(m) {
        return m.setIcon(marker.iconUrl);
      });
    };
    MarkerManager.prototype.addMarker = function(marker) {
      var markerPromise = this._mapsWrapper.createMarker({
        position: {
          lat: marker.latitude,
          lng: marker.longitude
        },
        label: marker.label,
        draggable: marker.draggable,
        icon: marker.iconUrl
      });
      this._markers.set(marker, markerPromise);
    };
    MarkerManager.prototype.getNativeMarker = function(marker) {
      return this._markers.get(marker);
    };
    MarkerManager.prototype.createEventObservable = function(eventName, marker) {
      var _this = this;
      return Observable_1.Observable.create(function(observer) {
        _this._markers.get(marker).then(function(m) {
          m.addListener(eventName, function(e) {
            return _this._zone.run(function() {
              return observer.next(e);
            });
          });
        });
      });
    };
    MarkerManager = __decorate([core_1.Injectable(), __metadata('design:paramtypes', [google_maps_api_wrapper_1.GoogleMapsAPIWrapper, core_1.NgZone])], MarkerManager);
    return MarkerManager;
  }());
  exports.MarkerManager = MarkerManager;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/info-window-manager.js", ["@angular/core", "./google-maps-api-wrapper", "./marker-manager"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var google_maps_api_wrapper_1 = $__require('./google-maps-api-wrapper');
  var marker_manager_1 = $__require('./marker-manager');
  var InfoWindowManager = (function() {
    function InfoWindowManager(_mapsWrapper, _zone, _markerManager) {
      this._mapsWrapper = _mapsWrapper;
      this._zone = _zone;
      this._markerManager = _markerManager;
      this._infoWindows = new Map();
    }
    InfoWindowManager.prototype.deleteInfoWindow = function(infoWindow) {
      var _this = this;
      var iWindow = this._infoWindows.get(infoWindow);
      if (iWindow == null) {
        return Promise.resolve();
      }
      return iWindow.then(function(i) {
        return _this._zone.run(function() {
          i.close();
          _this._infoWindows.delete(infoWindow);
        });
      });
    };
    InfoWindowManager.prototype.setPosition = function(infoWindow) {
      return this._infoWindows.get(infoWindow).then(function(i) {
        return i.setPosition({
          lat: infoWindow.latitude,
          lng: infoWindow.longitude
        });
      });
    };
    InfoWindowManager.prototype.setZIndex = function(infoWindow) {
      return this._infoWindows.get(infoWindow).then(function(i) {
        return i.setZIndex(infoWindow.zIndex);
      });
    };
    InfoWindowManager.prototype.open = function(infoWindow) {
      var _this = this;
      return this._infoWindows.get(infoWindow).then(function(w) {
        if (infoWindow.hostMarker != null) {
          return _this._markerManager.getNativeMarker(infoWindow.hostMarker).then(function(marker) {
            return _this._mapsWrapper.getMap().then(function(map) {
              return w.open(map, marker);
            });
          });
        }
        return _this._mapsWrapper.getMap().then(function(map) {
          return w.open(map);
        });
      });
    };
    InfoWindowManager.prototype.close = function(infoWindow) {
      return this._infoWindows.get(infoWindow).then(function(w) {
        return w.close();
      });
    };
    InfoWindowManager.prototype.setOptions = function(infoWindow, options) {
      return this._infoWindows.get(infoWindow).then(function(i) {
        return i.setOptions(options);
      });
    };
    InfoWindowManager.prototype.addInfoWindow = function(infoWindow) {
      var options = {content: infoWindow.content};
      if (typeof infoWindow.latitude === 'number' && typeof infoWindow.longitude === 'number') {
        options.position = {
          lat: infoWindow.latitude,
          lng: infoWindow.longitude
        };
      }
      var infoWindowPromise = this._mapsWrapper.createInfoWindow(options);
      this._infoWindows.set(infoWindow, infoWindowPromise);
    };
    InfoWindowManager = __decorate([core_1.Injectable(), __metadata('design:paramtypes', [google_maps_api_wrapper_1.GoogleMapsAPIWrapper, core_1.NgZone, marker_manager_1.MarkerManager])], InfoWindowManager);
    return InfoWindowManager;
  }());
  exports.InfoWindowManager = InfoWindowManager;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/maps-api-loader/maps-api-loader.js", ["@angular/core"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var core_1 = $__require('@angular/core');
  var MapsAPILoader = (function() {
    function MapsAPILoader() {}
    MapsAPILoader = __decorate([core_1.Injectable(), __metadata('design:paramtypes', [])], MapsAPILoader);
    return MapsAPILoader;
  }());
  exports.MapsAPILoader = MapsAPILoader;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services/maps-api-loader/lazy-maps-api-loader.js", ["@angular/core", "./maps-api-loader"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var __extends = (this && this.__extends) || function(d, b) {
    for (var p in b)
      if (b.hasOwnProperty(p))
        d[p] = b[p];
    function __() {
      this.constructor = d;
    }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
  };
  var __decorate = (this && this.__decorate) || function(decorators, target, key, desc) {
    var c = arguments.length,
        r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc,
        d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function")
      r = Reflect.decorate(decorators, target, key, desc);
    else
      for (var i = decorators.length - 1; i >= 0; i--)
        if (d = decorators[i])
          r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
  };
  var __metadata = (this && this.__metadata) || function(k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function")
      return Reflect.metadata(k, v);
  };
  var __param = (this && this.__param) || function(paramIndex, decorator) {
    return function(target, key) {
      decorator(target, key, paramIndex);
    };
  };
  var core_1 = $__require('@angular/core');
  var maps_api_loader_1 = $__require('./maps-api-loader');
  (function(GoogleMapsScriptProtocol) {
    GoogleMapsScriptProtocol[GoogleMapsScriptProtocol["HTTP"] = 0] = "HTTP";
    GoogleMapsScriptProtocol[GoogleMapsScriptProtocol["HTTPS"] = 1] = "HTTPS";
    GoogleMapsScriptProtocol[GoogleMapsScriptProtocol["AUTO"] = 2] = "AUTO";
  })(exports.GoogleMapsScriptProtocol || (exports.GoogleMapsScriptProtocol = {}));
  var GoogleMapsScriptProtocol = exports.GoogleMapsScriptProtocol;
  var LazyMapsAPILoaderConfig = (function() {
    function LazyMapsAPILoaderConfig() {
      this.apiKey = null;
      this.clientId = null;
      this.apiVersion = '3';
      this.hostAndPath = 'maps.googleapis.com/maps/api/js';
      this.protocol = GoogleMapsScriptProtocol.HTTPS;
      this.libraries = [];
      this.region = null;
      this.language = null;
    }
    return LazyMapsAPILoaderConfig;
  }());
  exports.LazyMapsAPILoaderConfig = LazyMapsAPILoaderConfig;
  var DEFAULT_CONFIGURATION = new LazyMapsAPILoaderConfig();
  var LazyMapsAPILoader = (function(_super) {
    __extends(LazyMapsAPILoader, _super);
    function LazyMapsAPILoader(_config) {
      _super.call(this);
      this._config = _config;
      if (this._config === null || this._config === undefined) {
        this._config = DEFAULT_CONFIGURATION;
      }
    }
    LazyMapsAPILoader.prototype.load = function() {
      if (this._scriptLoadingPromise) {
        return this._scriptLoadingPromise;
      }
      var script = document.createElement('script');
      script.type = 'text/javascript';
      script.async = true;
      script.defer = true;
      var callbackName = "angular2googlemaps" + new Date().getMilliseconds();
      script.src = this._getScriptSrc(callbackName);
      this._scriptLoadingPromise = new Promise(function(resolve, reject) {
        window[callbackName] = function() {
          resolve();
        };
        script.onerror = function(error) {
          reject(error);
        };
      });
      document.body.appendChild(script);
      return this._scriptLoadingPromise;
    };
    LazyMapsAPILoader.prototype._getScriptSrc = function(callbackName) {
      var protocolType = (this._config && this._config.protocol) || DEFAULT_CONFIGURATION.protocol;
      var protocol;
      switch (protocolType) {
        case GoogleMapsScriptProtocol.AUTO:
          protocol = '';
          break;
        case GoogleMapsScriptProtocol.HTTP:
          protocol = 'http:';
          break;
        case GoogleMapsScriptProtocol.HTTPS:
          protocol = 'https:';
          break;
      }
      var hostAndPath = this._config.hostAndPath || DEFAULT_CONFIGURATION.hostAndPath;
      var apiKey = this._config.apiKey || DEFAULT_CONFIGURATION.apiKey;
      var clientId = this._config.clientId || DEFAULT_CONFIGURATION.clientId;
      var libraries = this._config.libraries || DEFAULT_CONFIGURATION.libraries;
      var region = this._config.region || DEFAULT_CONFIGURATION.region;
      var language = this._config.language || DEFAULT_CONFIGURATION.language;
      var queryParams = {
        v: this._config.apiVersion || DEFAULT_CONFIGURATION.apiVersion,
        callback: callbackName
      };
      if (apiKey) {
        queryParams['key'] = apiKey;
      }
      if (clientId) {
        queryParams['client'] = clientId;
      }
      if (libraries != null && libraries.length > 0) {
        queryParams['libraries'] = libraries.join(',');
      }
      if (region != null && region.length > 0) {
        queryParams['region'] = region;
      }
      if (language != null && language.length > 0) {
        queryParams['language'] = language;
      }
      var params = Object.keys(queryParams).map(function(k, i) {
        var param = (i === 0) ? '?' : '&';
        return param += k + "=" + queryParams[k];
      }).join('');
      return protocol + "//" + hostAndPath + params;
    };
    LazyMapsAPILoader = __decorate([core_1.Injectable(), __param(0, core_1.Optional()), __metadata('design:paramtypes', [LazyMapsAPILoaderConfig])], LazyMapsAPILoader);
    return LazyMapsAPILoader;
  }(maps_api_loader_1.MapsAPILoader));
  exports.LazyMapsAPILoader = LazyMapsAPILoader;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/services.js", ["./services/maps-api-loader/maps-api-loader", "./services/maps-api-loader/noop-maps-api-loader", "./services/google-maps-api-wrapper", "./services/marker-manager", "./services/info-window-manager", "./services/maps-api-loader/lazy-maps-api-loader"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  var maps_api_loader_1 = $__require('./services/maps-api-loader/maps-api-loader');
  exports.MapsAPILoader = maps_api_loader_1.MapsAPILoader;
  var noop_maps_api_loader_1 = $__require('./services/maps-api-loader/noop-maps-api-loader');
  exports.NoOpMapsAPILoader = noop_maps_api_loader_1.NoOpMapsAPILoader;
  var google_maps_api_wrapper_1 = $__require('./services/google-maps-api-wrapper');
  exports.GoogleMapsAPIWrapper = google_maps_api_wrapper_1.GoogleMapsAPIWrapper;
  var marker_manager_1 = $__require('./services/marker-manager');
  exports.MarkerManager = marker_manager_1.MarkerManager;
  var info_window_manager_1 = $__require('./services/info-window-manager');
  exports.InfoWindowManager = info_window_manager_1.InfoWindowManager;
  var lazy_maps_api_loader_1 = $__require('./services/maps-api-loader/lazy-maps-api-loader');
  exports.LazyMapsAPILoader = lazy_maps_api_loader_1.LazyMapsAPILoader;
  exports.LazyMapsAPILoaderConfig = lazy_maps_api_loader_1.LazyMapsAPILoaderConfig;
  exports.GoogleMapsScriptProtocol = lazy_maps_api_loader_1.GoogleMapsScriptProtocol;
  return module.exports;
});

System.registerDynamic("angular2-google-maps/core.js", ["@angular/core", "./services/maps-api-loader/maps-api-loader", "./services/maps-api-loader/lazy-maps-api-loader", "./directives", "./services"], true, function($__require, exports, module) {
  "use strict";
  ;
  var define,
      global = this,
      GLOBAL = this;
  function __export(m) {
    for (var p in m)
      if (!exports.hasOwnProperty(p))
        exports[p] = m[p];
  }
  var core_1 = $__require('@angular/core');
  var maps_api_loader_1 = $__require('./services/maps-api-loader/maps-api-loader');
  var lazy_maps_api_loader_1 = $__require('./services/maps-api-loader/lazy-maps-api-loader');
  __export($__require('./directives'));
  __export($__require('./services'));
  exports.ANGULAR2_GOOGLE_MAPS_PROVIDERS = [new core_1.Provider(maps_api_loader_1.MapsAPILoader, {useClass: lazy_maps_api_loader_1.LazyMapsAPILoader})];
  return module.exports;
});

//# sourceMappingURL=angular2-google-maps.js.map