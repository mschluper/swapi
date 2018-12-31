"# swapi" 
This is a response to https://github.com/platt-blackops/PlattCodeSampleApp.

The solution targets .NET Core 2.1.

On the plus side it processes requests asynchronously and concurrently where possible. 
It also has a simple response cache to avoid accessing swapi twice for the same Get request.

On the down side it lacks a test project and it lacks error handling.
