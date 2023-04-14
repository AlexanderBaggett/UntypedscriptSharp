# UntypedSharp

### A strongly untyped framework


 UntypedSharped is a compact micro-framework to bring untyped javascript features to the .NET world
 
 It started out as a fun experiment with implicit operators in C#, and evolved into this.
 
 
 ## Any
 A class representing the Javascript any type. (let the bullshittery commence) or as close as C# will let us emulate it.
 
 
 ## Falsy checking
 We can simulate falsy checking in Javascript with C# and .Net
 `Any<string> any = "stuff"`
 `if(any)`  is equivalent to `if(any!= null || any != "")` etc
 

 ## Adhoc properties
`Any<string> any = "adsf"`
`any["stuff"] = "more stuff";`

## Operators

`any > 10`
`10 < any`
`true > any`
`any + "try this at home kids"`
..etc

