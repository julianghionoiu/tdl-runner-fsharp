open TDL.Client
open TDL.Client.Runner
open BeFaster.App.Solutions.CHK
open BeFaster.App.Solutions.FIZ
open BeFaster.App.Solutions.HLO
open BeFaster.App.Solutions.SUM
open BeFaster.App.Solutions.ARRS
open BeFaster.App.Solutions.IRNG
open BeFaster.Runner
open BeFaster.Runner.Extensions
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
///      msbuild befaster.sln; src\BeFaster.App\bin\Debug\BeFaster.App.exe
///       or
///      msbuild befaster.sln; mono src/BeFaster.App/bin/Debug/BeFaster.App.exe
///
///   To run your unit tests locally:
///      Run the "BeFaster.App.Tests - Unit Tests" configuration.
///
/// ~~~~~~~~~~ The workflow ~~~~~~~~~~~~~
///
///   By running this file you interact with a challenge server.
///   The interaction follows a request-response pattern:
///        * You are presented with your current progress and a list of actions.
///        * You trigger one of the actions by typing it on the console.
///        * After the action feedback is presented, the execution will stop.
///
///   +------+-------------------------------------------------------------+
///   | Step | The usual workflow                                          |
///   +------+-------------------------------------------------------------+
///   |  1.  | Run this file.                                              |
///   |  2.  | Start a challenge by typing "start".                        |
///   |  3.  | Read description from the "challenges" folder               |
///   |  4.  | Implement the required method in                            |
///   |      |   .\src\BeFaster.App\Solutions                              |
///   |  5.  | Deploy to production by typing "deploy".                    |
///   |  6.  | Observe output, check for failed requests.                  |
///   |  7.  | If passed, go to step 3.                                    |
///   +------+-------------------------------------------------------------+
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
            .WithSolutionFor("sum", fun p -> Sum.sum((int)p.[0], (int)p.[1]) :> obj)
            .WithSolutionFor("hello", fun p -> Hello.hello((string)p.[0]) :> obj)
            .WithSolutionFor("array_sum", fun p -> ArraySum.compute(p.[0].ToObject<List<int>>()) :> obj)
            .WithSolutionFor("int_range", fun p -> IntRange.generate((int)p.[0], (int)p.[1]) :> obj)
            .WithSolutionFor("fizz_buzz", fun p -> FizzBuzz.fizzBuzz((int)p.[0]) :> obj)
            .WithSolutionFor("checkout", fun p -> Checkout.checkout((string)p.[0]) :> obj)
            .Create()

    ChallengeSession.ForRunner(runner)
        .WithConfig(Utils.GetConfig())
        .WithActionProvider(new UserInputAction(argv))
        .Start()

    printf "Press any key to continue . . . "
    Console.ReadKey() |> ignore
    
    0 // return an integer exit code
