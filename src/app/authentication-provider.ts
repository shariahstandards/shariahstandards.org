import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
import {AuthenticatedHttpService} from './authenticated-http.service'
export class AuthenticationProvider
{
  provide:Http,
  useFactory:AuthenticationServiceUseFactory,
  deps: [XHRBackend, RequestOptions]
};

var AuthenticationServiceUseFactory=function(backend: XHRBackend, defaultOptions: RequestOptions) {
    new AuthenticatedHttpService(backend, defaultOptions);
};

export AuthenticationServiceUseFactory