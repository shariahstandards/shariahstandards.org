import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
import {AuthenticatedHttpService} from './authenticated-http.service'



// export var AuthenticationServiceUseFactory=function(backend: XHRBackend, defaultOptions: RequestOptions) {
//     new AuthenticatedHttpService(backend, defaultOptions);
// };
export class AuthenticationProvider
{
  provide:Http;
  useFactory:AuthenticatedHttpService;
  deps: [XHRBackend, RequestOptions]
};
