using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Inflector;

namespace Services
{
  public class CustomDesignTimeServices : IDesignTimeServices
  {
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
      services.AddSingleton<IPluralizer, CustomPluralizer>();
    }
  }
  public class CustomPluralizer : IPluralizer
  {
    public string Pluralize(string name)
    {
      return name.Pluralize() ?? name;
    }
    public string Singularize(string name)
    {
      return name.Singularize() ?? name;
    }
  }
}
