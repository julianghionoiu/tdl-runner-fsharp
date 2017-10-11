namespace BeFaster.App.Solutions

open System

type HelloSolution private() =
    static member Hello(friendName : string) = 
        raise (NotImplementedException())
