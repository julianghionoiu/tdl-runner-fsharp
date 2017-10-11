using System;
using System.Collections.Generic;

namespace BeFaster.Runner
{
    public partial class ClientRunner
    {
        public class Builder
        {
            private string username;
            private string hostname;
            private RunnerAction defaultRunnerAction;
            private readonly SolutionsBuilder solutionsBuilder = new SolutionsBuilder();

            public Builder ForUsername(string username)
            {
                this.username = username;
                return this;
            }

            public Builder WithServerHostname(string hostname)
            {
                this.hostname = hostname;
                return this;
            }

            public Builder WithActionIfNoArgs(RunnerAction defaultRunnerAction)
            {
                this.defaultRunnerAction = defaultRunnerAction;
                return this;
            }

            public Builder WithSolutions(Action<SolutionsBuilder> applySolutions)
            {
                applySolutions(solutionsBuilder);
                return this;
            }

            public ClientRunner Create() => new ClientRunner(username, hostname, defaultRunnerAction, solutionsBuilder.Solutions);
        }

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
