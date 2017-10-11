namespace BeFaster.App.Tests

open NUnit.Framework
open BeFaster.App.Solutions

[<TestFixture>]
type SumSolutionTests() = 
    
    [<Test>]
    member x.``2 + 3 = 5``() =
        Assert.AreEqual(5, SumSolution.Sum(2, 3))
