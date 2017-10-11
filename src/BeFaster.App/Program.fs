open BeFaster.Runner
open BeFaster.Runner.Extensions
open BeFaster.App.Solutions

[<EntryPoint>]
let main argv = 
    ClientRunner
        .Build()
        .ForUsername(CredentialsConfigFile.Get("tdl_username"))
        .WithServerHostname("run.befaster.io")
        .WithActionIfNoArgs(RunnerAction.TestConnectivity)
        .WithSolutions(fun s ->
            s.On("checkout").Call(fun p -> CheckoutSolution.Checkout(p.[0]))
            s.On("hello").Call(fun p -> HelloSolution.Hello(p.[0]))
            s.On("fizz_buzz").Call(fun p -> FizzBuzzSolution.FizzBuzz(p.[0].AsInt()))
            s.On("sum").Call(fun p -> SumSolution.Sum(p.[0].AsInt(), p.[1].AsInt())))
        .Create()
        .Start(argv)
    0 // return an integer exit code
