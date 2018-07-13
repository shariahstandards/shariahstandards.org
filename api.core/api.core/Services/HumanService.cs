using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebApiResources;

namespace Services
{
    public interface IHumanServiceDependencies
    {
    }
    public class HumanServiceDependencies : IHumanServiceDependencies
    {
    }
    public interface IHumanService
    {
        ResponseResource RegisterAHumanBeing(IPrincipal user, RegisterAHumanBeingRequest request);
    }
    public class HumanService: IHumanService
    {
        private IHumanServiceDependencies _dependencies;
        public HumanService(IHumanServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual ResponseResource RegisterAHumanBeing(IPrincipal user, RegisterAHumanBeingRequest request)
        {

            throw new NotImplementedException();
        }
    }
}
