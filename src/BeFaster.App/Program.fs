// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open BeFaster.Runner
open BeFaster.App.Solutions

[<EntryPoint>]
let main argv = 
    ClientRunner
        .Build()
        .ForUsername(CredentialsConfigFile.Get("tdl_username"))
        .WithServerHostname("run.befaster.io")
        .WithActionIfNoArgs(RunnerAction.TestConnectivity)
        .Create()
        .Start(argv)
    0 // return an integer exit code
