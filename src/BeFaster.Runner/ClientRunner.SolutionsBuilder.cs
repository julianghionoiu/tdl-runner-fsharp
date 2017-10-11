using System;
using System.Collections.Generic;

namespace BeFaster.Runner
{
    public partial class ClientRunner
    {
        public class SolutionsBuilder
        {
            public Dictionary<string, Func<string[], object>> Solutions { get; } = new Dictionary<string, Func<string[], object>>();

            public SolutionBuilder On(string methodName)
            {
                return new SolutionBuilder(methodName, this);
            }

            public class SolutionBuilder
            {
                private readonly SolutionsBuilder solutionsBuilder;
                private readonly string methodName;

                public SolutionBuilder(string methodName, SolutionsBuilder solutionsBuilder)
                {
                    this.methodName = methodName;
                    this.solutionsBuilder = solutionsBuilder;
                }

                public void Call(Func<string[], object> solution)
                {
                    solutionsBuilder.Solutions.Add(methodName, solution);
                }
            }
        }
    }
}
