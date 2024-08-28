open TDL.Client
open TDL.Client.Runner
open BeFaster.App.Solutions.CHK
open BeFaster.App.Solutions.CHL
open BeFaster.App.Solutions.FIZ
open BeFaster.App.Solutions.HLO
open BeFaster.App.Solutions.SUM
open BeFaster.App.Solutions.ARRS
open BeFaster.App.Solutions.IRNG
open BeFaster.Runner
open BeFaster.Runner.Utils
open Newtonsoft.Json.Linq;
open System

/// <summary>
/// ~~~~~~~~~~ Running the system: ~~~~~~~~~~~~~
///
///   From IDE:
///      Configure the "BeFaster.App" solution to Run on External Console then run.
///
///   From command line:
///      dotnet run --project src\BeFaster.App
///
///   To run your unit tests locally:
///      Run the "BeFaster.App.Tests" project.
///      or
///      dotnet test
///
/// ~~~~~~~~~~ The workflow ~~~~~~~~~~~~~
///
///   By running this file you interact with a challenge server.
///   The interaction follows a request-response pattern:
///        * You are presented with your current progress and a list of actions.
///        * You trigger one of the actions by typing it on the console.
///        * After the action feedback is presented, the execution will stop.
///
///   +------+-----------------------------------------------------------------------+
///   | Step | The usual workflow                                                    |
///   +------+-----------------------------------------------------------------------+
///   |  1.  | Run this file.                                                        |
///   |  2.  | Start a challenge by typing "start".                                  |
///   |  3.  | Read the description from the "challenges" folder.                    |
///   |  4.  | Locate the file corresponding to your current challenge in:           |
///   |      |   src\BeFaster.App\Solutions                                          |
///   |  5.  | Replace the following placeholder exception with your solution:       |
///   |      |   raise (NotImplementedException())                                   |
///   |  6.  | Deploy to production by typing "deploy".                              |
///   |  7.  | Observe the output, check for failed requests.                        |
///   |  8.  | If passed, go to step 1.                                              |
///   +------+-----------------------------------------------------------------------+
///
///   You are encouraged to change this project as you please:
///        * You can use your preferred libraries.
///        * You can use your own test framework.
///        * You can change the file structure.
///        * Anything really, provided that this file stays runnable.
///
/// </summary>
[<EntryPoint>]
let main argv = 
    let runner =
        QueueBasedImplementationRunner.Builder()
            .SetConfig(Utils.Utils.GetRunnerConfig())
            .WithSolutionFor("sum", fun p -> Sum.sum(p.[0].ToObject<int>(), p.[1].ToObject<int>()) :> obj)
            .WithSolutionFor("hello", fun p -> Hello.hello(p.[0].ToObject<string>()) :> obj)
            .WithSolutionFor("array_sum", fun p -> ArraySum.compute(p.[0].ToObject<List<int>>()) :> obj)
            .WithSolutionFor("int_range", fun p -> IntRange.generate(p.[0].ToObject<int>(), p.[1].ToObject<int>()) :> obj)
            .WithSolutionFor("fizz_buzz", fun p -> FizzBuzz.fizzBuzz(p.[0].ToObject<int>()) :> obj)
            .WithSolutionFor("checkout", fun p -> Checkout.computePrice(p.[0].ToObject<string>()) :> obj)
            .WithSolutionFor("checklite", fun p -> Checklite.computePrice(p.[0].ToObject<string>()) :> obj)
            .Create()

    ChallengeSession.ForRunner(runner)
        .WithConfig(Utils.GetConfig())
        .WithActionProvider(new UserInputAction(argv))
        .Start()

    printf "Press any key to continue . . . "
    Console.ReadKey() |> ignore
    
    0 // return an integer exit code
