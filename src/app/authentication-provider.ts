import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
import {AuthenticatedHttpService} from './authenticated-http.service'


var AuthenticationServiceUseFactory=function(backend: XHRBackend, defaultOptions: RequestOptions) {
    new AuthenticatedHttpService(backend, defaultOptions);
};
export class AuthenticationProvider
{
  provide:Http,
  useFactory:AuthenticationServiceUseFactory,
  deps: [XHRBackend, RequestOptions]
};
export AuthenticationServiceUseFactory