using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System;

namespace PayrollRestApiTest
{
    [TestClass]
    public class TestRestAPI
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        /// <summary>
        /// Gets the employee list in the form of irestresponse. 
        /// </summary>
        /// <returns>IRestResponse response</returns>
        private IRestResponse GetEmployeeDetails()
        {
            //makes restrequest for getting all the data from json server by giving table name and method.get
            RestRequest request = new RestRequest("/employees", Method.GET);
            //executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// UC1
        /// Ons the calling get API return employee list.
        /// </summary>
        [TestMethod]
        public void MakingGetRequestToReturnIRestResponseObject()
        {
            //Arrange
            IRestResponse response = GetEmployeeDetails();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //adding the data into list from irestresponse by using deserializing.
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(5, dataResponse.Count);
            foreach (Employee employee in dataResponse)
            {
                Console.WriteLine("Id: " + employee.id + " Name: " + employee.name + " Salary: " + employee.salary);
            }
        }
    }
}
