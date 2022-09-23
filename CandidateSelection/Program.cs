// See https://aka.ms/new-console-template for more information

using AutoMapper;
using CandidateSelection;
using CandidateSelection.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

//ConfigureServices
var services = new ServiceCollection();
services.AddScoped<IRecruit, RecruitRepository>();

IMapper mapper;
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new RecruitMappings());
});
mapper = mappingConfig.CreateMapper();
services.AddAutoMapper(typeof(RecruitMappings));

var callingObj = services.BuildServiceProvider().GetRequiredService <IRecruit>(); 

//Main Program Execution
Console.WriteLine("This is the list of technologies:");

var technames = await callingObj.GetTechnologyNames();
foreach (var name in technames)
Console.WriteLine(name.ToString());

Console.WriteLine("Please enter exact name of technology or copy it from above list :");
string? technologyName = Console.ReadLine();
Console.WriteLine("Please enter years of experience :");
string experience = Console.ReadLine();

if (!String.IsNullOrWhiteSpace(technologyName) && !String.IsNullOrWhiteSpace(experience))
{
    var getCandidateDetails = JsonConvert.SerializeObject(callingObj.GetCandidateDetails(technologyName, experience));
    if (getCandidateDetails.Length == 2 || getCandidateDetails=="null")
    {
        Console.WriteLine("No Candidate Found.Please restart the search from the begining.");
        Console.ReadKey();
        Environment.Exit(0);
    }

    Console.WriteLine("\nThese are filtered candidates: {0}", getCandidateDetails);
}


Console.WriteLine("\nPlease enter id of candidates you want to select (seperated by space), you can paste it from above:");
string selectedId = Console.ReadLine();
if (!String.IsNullOrWhiteSpace(selectedId))
{
    Console.WriteLine("\nThese are Selected candidates: {0}", JsonConvert.SerializeObject(callingObj.GetSelectedCandidates(selectedId)));
}

Console.WriteLine("\nPlease enter id of candidates you want to reject (seperated by space), you can paste it from above:");
string rejectedId = Console.ReadLine();
if (!String.IsNullOrWhiteSpace(selectedId) || !String.IsNullOrWhiteSpace(rejectedId))
{
    callingObj.RemoveCandidates(selectedId, rejectedId);
}

Console.WriteLine("\nThis Search Completed Sucessfully");
