namespace ScheduleBox.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using ScheduleBox.Controllers;
    using ScheduleBox.Model.PizzaCabinApiResponse;

    public class SchedulesControllerTests
    {
        [Test]
        public async Task CallsPizzaCabinWithCorrectUri()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"ScheduleResult\":{\"Schedules\":null}}",
                        Encoding.UTF8,
                        "application/json"),
                });
            var controller = new SchedulesController(new PizzaCabinClient(new HttpClient(fakeHttpMessageHandler)));

            _ = await controller.Get(new DateTime(2015, 12, 14));
            Assert.AreEqual("http://pizzacabininc.azurewebsites.net/PizzaCabinInc.svc/schedule/2015-12-14", fakeHttpMessageHandler.Request.RequestUri.AbsoluteUri);
        }

        [Test]
        public async Task WhenNoSchedules()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"ScheduleResult\":{\"Schedules\":null}}",
                        Encoding.UTF8,
                        "application/json"),
                });
            var controller = new SchedulesController(new PizzaCabinClient(new HttpClient(fakeHttpMessageHandler)));

            var actionResult = await controller.Get(new DateTime(2015, 12, 14));
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
            var notFound = (NotFoundObjectResult)actionResult.Result;
            Assert.AreEqual("No schedules found for selected date. Please try again.", notFound.Value);
        }

        [Test]
        public async Task WhenHasSchedules()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"ScheduleResult\":{\"Schedules\":[{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Daniel Billsus\",\"PersonId\":\"4fd900ad-2b33-469c-87ac-9b5e015b2564\",\"Projection\":[{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450080000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450087200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450088100000+0000)\\/\",\"minutes\":105},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450094400000+0000)\\/\",\"minutes\":60},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450098000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450106100000+0000)\\/\",\"minutes\":105}]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Michael Kantor\",\"PersonId\":\"d56e32cc-2e46-4ba2-9fc1-9b5e015b2572\",\"Projection\":[{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450090800000+0000)\\/\",\"minutes\":150},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450099800000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450100700000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":60},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450108800000+0000)\\/\",\"minutes\":90},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450114200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450115100000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFC080\",\"Description\":\"Chat\",\"Start\":\"\\/Date(1450119600000+0000)\\/\",\"minutes\":60}]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Bill Gates\",\"PersonId\":\"826f2a46-93bb-4b04-8d5e-9b5e015b2577\",\"Projection\":[{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450090800000+0000)\\/\",\"minutes\":150},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450099800000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450100700000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":60},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450108800000+0000)\\/\",\"minutes\":90},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450114200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450115100000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFC080\",\"Description\":\"Chat\",\"Start\":\"\\/Date(1450119600000+0000)\\/\",\"minutes\":60}]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Candy Mamer\",\"PersonId\":\"2856c6cf-5c6a-4379-8c52-9b5e015b2580\",\"Projection\":[{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450090800000+0000)\\/\",\"minutes\":150},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450099800000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450100700000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":60},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450108800000+0000)\\/\",\"minutes\":90},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450114200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450115100000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFC080\",\"Description\":\"Chat\",\"Start\":\"\\/Date(1450119600000+0000)\\/\",\"minutes\":60}]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Tim McMahon\",\"PersonId\":\"3833e4a7-dbf4-4130-9027-9b5e015b2580\",\"Projection\":[{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450090800000+0000)\\/\",\"minutes\":150},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450099800000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450100700000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":60},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450108800000+0000)\\/\",\"minutes\":90},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450114200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450115100000+0000)\\/\",\"minutes\":75},{\"Color\":\"#FFC080\",\"Description\":\"Chat\",\"Start\":\"\\/Date(1450119600000+0000)\\/\",\"minutes\":60}]},{\"ContractTimeMinutes\":0,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Sharad Mehrotra\",\"PersonId\":\"637ab62a-a0c1-49f3-a475-9b5e015b2580\",\"Projection\":[]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"George Lueker\",\"PersonId\":\"71d27b06-30c0-49fd-ae16-9b5e015b2580\",\"Projection\":[{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450080000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450087200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450088100000+0000)\\/\",\"minutes\":105},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450094400000+0000)\\/\",\"minutes\":60},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450098000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450106100000+0000)\\/\",\"minutes\":105}]},{\"ContractTimeMinutes\":0,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Steve Novack\",\"PersonId\":\"1a714f36-ee87-4a06-88d6-9b5e015b2585\",\"Projection\":[]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Kari Nies\",\"PersonId\":\"d1a6cf64-ecce-4b8a-ab03-9b5e015b2585\",\"Projection\":[{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450080000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450087200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450088100000+0000)\\/\",\"minutes\":105},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450094400000+0000)\\/\",\"minutes\":60},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450098000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450106100000+0000)\\/\",\"minutes\":105}]},{\"ContractTimeMinutes\":480,\"Date\":\"\\/Date(1450051200000+0000)\\/\",\"IsFullDayAbsence\":false,\"Name\":\"Carlos Oliveira\",\"PersonId\":\"e60babbe-29f1-4b61-bba2-9b5e015b2585\",\"Projection\":[{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450080000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450087200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450088100000+0000)\\/\",\"minutes\":105},{\"Color\":\"#FFFF00\",\"Description\":\"Lunch\",\"Start\":\"\\/Date(1450094400000+0000)\\/\",\"minutes\":60},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450098000000+0000)\\/\",\"minutes\":120},{\"Color\":\"#FF0000\",\"Description\":\"Short break\",\"Start\":\"\\/Date(1450105200000+0000)\\/\",\"minutes\":15},{\"Color\":\"#80FF80\",\"Description\":\"Phone\",\"Start\":\"\\/Date(1450106100000+0000)\\/\",\"minutes\":45},{\"Color\":\"#1E90FF\",\"Description\":\"Social Media\",\"Start\":\"\\/Date(1450108800000+0000)\\/\",\"minutes\":60}]}]}}",
                        Encoding.UTF8,
                        "application/json"),
                });
            var controller = new SchedulesController(new PizzaCabinClient(new HttpClient(fakeHttpMessageHandler)));

            var actionResult = await controller.Get(new DateTime(2015, 12, 14));
            var expected = @"{
  ""Start"": ""2015-12-14T08:00:00+00:00"",
  ""End"": ""2015-12-14T20:00:00+00:00"",
  ""Schedules"": [
    {
      ""Person"": {
        ""Name"": ""Daniel Billsus"",
        ""Id"": ""4fd900ad-2b33-469c-87ac-9b5e015b2564""
      },
      ""Activities"": [
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T08:00:00+00:00"",
          ""End"": ""2015-12-14T10:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T10:00:00+00:00"",
          ""End"": ""2015-12-14T10:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T10:15:00+00:00"",
          ""End"": ""2015-12-14T12:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T12:00:00+00:00"",
          ""End"": ""2015-12-14T13:00:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T13:00:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T15:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T15:15:00+00:00"",
          ""End"": ""2015-12-14T17:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Michael Kantor"",
        ""Id"": ""d56e32cc-2e46-4ba2-9fc1-9b5e015b2572""
      },
      ""Activities"": [
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T11:00:00+00:00"",
          ""End"": ""2015-12-14T13:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T13:30:00+00:00"",
          ""End"": ""2015-12-14T13:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T13:45:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T16:00:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T16:00:00+00:00"",
          ""End"": ""2015-12-14T17:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T17:30:00+00:00"",
          ""End"": ""2015-12-14T17:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T17:45:00+00:00"",
          ""End"": ""2015-12-14T19:00:00+00:00""
        },
        {
          ""Description"": ""Chat"",
          ""Color"": ""#FFC080"",
          ""Start"": ""2015-12-14T19:00:00+00:00"",
          ""End"": ""2015-12-14T20:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Bill Gates"",
        ""Id"": ""826f2a46-93bb-4b04-8d5e-9b5e015b2577""
      },
      ""Activities"": [
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T11:00:00+00:00"",
          ""End"": ""2015-12-14T13:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T13:30:00+00:00"",
          ""End"": ""2015-12-14T13:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T13:45:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T16:00:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T16:00:00+00:00"",
          ""End"": ""2015-12-14T17:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T17:30:00+00:00"",
          ""End"": ""2015-12-14T17:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T17:45:00+00:00"",
          ""End"": ""2015-12-14T19:00:00+00:00""
        },
        {
          ""Description"": ""Chat"",
          ""Color"": ""#FFC080"",
          ""Start"": ""2015-12-14T19:00:00+00:00"",
          ""End"": ""2015-12-14T20:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Candy Mamer"",
        ""Id"": ""2856c6cf-5c6a-4379-8c52-9b5e015b2580""
      },
      ""Activities"": [
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T11:00:00+00:00"",
          ""End"": ""2015-12-14T13:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T13:30:00+00:00"",
          ""End"": ""2015-12-14T13:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T13:45:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T16:00:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T16:00:00+00:00"",
          ""End"": ""2015-12-14T17:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T17:30:00+00:00"",
          ""End"": ""2015-12-14T17:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T17:45:00+00:00"",
          ""End"": ""2015-12-14T19:00:00+00:00""
        },
        {
          ""Description"": ""Chat"",
          ""Color"": ""#FFC080"",
          ""Start"": ""2015-12-14T19:00:00+00:00"",
          ""End"": ""2015-12-14T20:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Tim McMahon"",
        ""Id"": ""3833e4a7-dbf4-4130-9027-9b5e015b2580""
      },
      ""Activities"": [
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T11:00:00+00:00"",
          ""End"": ""2015-12-14T13:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T13:30:00+00:00"",
          ""End"": ""2015-12-14T13:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T13:45:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T16:00:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T16:00:00+00:00"",
          ""End"": ""2015-12-14T17:30:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T17:30:00+00:00"",
          ""End"": ""2015-12-14T17:45:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T17:45:00+00:00"",
          ""End"": ""2015-12-14T19:00:00+00:00""
        },
        {
          ""Description"": ""Chat"",
          ""Color"": ""#FFC080"",
          ""Start"": ""2015-12-14T19:00:00+00:00"",
          ""End"": ""2015-12-14T20:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Sharad Mehrotra"",
        ""Id"": ""637ab62a-a0c1-49f3-a475-9b5e015b2580""
      },
      ""Activities"": []
    },
    {
      ""Person"": {
        ""Name"": ""George Lueker"",
        ""Id"": ""71d27b06-30c0-49fd-ae16-9b5e015b2580""
      },
      ""Activities"": [
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T08:00:00+00:00"",
          ""End"": ""2015-12-14T10:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T10:00:00+00:00"",
          ""End"": ""2015-12-14T10:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T10:15:00+00:00"",
          ""End"": ""2015-12-14T12:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T12:00:00+00:00"",
          ""End"": ""2015-12-14T13:00:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T13:00:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T15:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T15:15:00+00:00"",
          ""End"": ""2015-12-14T17:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Steve Novack"",
        ""Id"": ""1a714f36-ee87-4a06-88d6-9b5e015b2585""
      },
      ""Activities"": []
    },
    {
      ""Person"": {
        ""Name"": ""Kari Nies"",
        ""Id"": ""d1a6cf64-ecce-4b8a-ab03-9b5e015b2585""
      },
      ""Activities"": [
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T08:00:00+00:00"",
          ""End"": ""2015-12-14T10:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T10:00:00+00:00"",
          ""End"": ""2015-12-14T10:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T10:15:00+00:00"",
          ""End"": ""2015-12-14T12:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T12:00:00+00:00"",
          ""End"": ""2015-12-14T13:00:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T13:00:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T15:15:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T15:15:00+00:00"",
          ""End"": ""2015-12-14T17:00:00+00:00""
        }
      ]
    },
    {
      ""Person"": {
        ""Name"": ""Carlos Oliveira"",
        ""Id"": ""e60babbe-29f1-4b61-bba2-9b5e015b2585""
      },
      ""Activities"": [
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T08:00:00+00:00"",
          ""End"": ""2015-12-14T10:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T10:00:00+00:00"",
          ""End"": ""2015-12-14T10:15:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T10:15:00+00:00"",
          ""End"": ""2015-12-14T12:00:00+00:00""
        },
        {
          ""Description"": ""Lunch"",
          ""Color"": ""#FFFF00"",
          ""Start"": ""2015-12-14T12:00:00+00:00"",
          ""End"": ""2015-12-14T13:00:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T13:00:00+00:00"",
          ""End"": ""2015-12-14T15:00:00+00:00""
        },
        {
          ""Description"": ""Short break"",
          ""Color"": ""#FF0000"",
          ""Start"": ""2015-12-14T15:00:00+00:00"",
          ""End"": ""2015-12-14T15:15:00+00:00""
        },
        {
          ""Description"": ""Phone"",
          ""Color"": ""#80FF80"",
          ""Start"": ""2015-12-14T15:15:00+00:00"",
          ""End"": ""2015-12-14T16:00:00+00:00""
        },
        {
          ""Description"": ""Social Media"",
          ""Color"": ""#1E90FF"",
          ""Start"": ""2015-12-14T16:00:00+00:00"",
          ""End"": ""2015-12-14T17:00:00+00:00""
        }
      ]
    }
  ]
}";
            Assert.AreEqual(expected, JsonConvert.SerializeObject(actionResult.Value, Formatting.Indented));
        }
    }
}