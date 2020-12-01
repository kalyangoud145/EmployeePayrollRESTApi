using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System;
using EmployeePayrollRestApi;
using Newtonsoft.Json.Linq;

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
        /* /// <summary>
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
         public void OnCallingList_ReturnEmployeeList()
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
         /// <summary>
         /// UC2
         /// Tests the add data by post operation.
         /// </summary>
         [TestMethod]
         public void GivenEmployee_OnPost_ShouldReturnAddedEmployee()
         {
             //Arrange
             //adding request to post(add) data
             RestRequest request = new RestRequest("/employees", Method.POST);
             //instatiating jObject for adding data for name and salary, id auto increments
             JObject jObject = new JObject();
             jObject.Add("id", 6);
             jObject.Add("name", "Rohit Sharma");
             jObject.Add("salary", "150000");
             //as parameters are passed as body hence "request body" call is made, in parameter type
             request.AddParameter("application/json", jObject, ParameterType.RequestBody);
             //Act
             //request contains method of post and along with added parameter which contains data to be added
             //hence response will contain the data which is added and not all the data from jsonserver.
             //data is added to json server json file in this step.
             IRestResponse response = client.Execute(request);

             //Assert
             Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
             //derserializing object for assert and checking test case
             Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
             Assert.AreEqual("Rohit Sharma", dataResponse.name);
             Assert.AreEqual("150000", dataResponse.salary);
             Console.WriteLine(response.Content);
         }
         /// <summary>
         /// UC3
         /// Tests the add multiple entries using post operation.
         /// </summary>
         [TestMethod]
         public void GivenMultipleEmployee_OnPost_ShouldRetrunAddedEmployees()
         {
             //adding multiple employees to table
             List<Employee> employeeList = new List<Employee>();
             employeeList.Add(new Employee { id = 7, name = "Virat Kohli", salary = "400000" });
             employeeList.Add(new Employee { id = 8, name = "MSD", salary = "500000" });
             foreach (Employee employee in employeeList)
             {
                 RestRequest request = new RestRequest("/employees", Method.POST);
                 JObject jObject = new JObject();
                 jObject.Add("name", employee.name);
                 jObject.Add("salary", employee.salary);
                 request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                 IRestResponse response = client.Execute(request);
                 //Assert
                 Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                 //derserializing object for assert and checking test case
                 Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
                 Assert.AreEqual(employee.name, dataResponse.name);
             }
         }*/
        /// <summary>
        /// UC4
        /// Tests the update data using put operation.
        /// </summary>
        [TestMethod]
        public void GivenEmployee_OnUpdate_ShouldReturnUpdatedEmployee()
        {
            //making a request for a particular employee to be updated
            RestRequest request = new RestRequest("employees/8", Method.PUT);
            //creating a jobject for new data to be added in place of old
            //json represents a new json object
            JObject jobject = new JObject();
            jobject.Add("name", "Tom");
            jobject.Add("salary", 5550000);
            //adding parameters in request
            //request body parameter type signifies values added using add.
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            //executing request using client
            //IRest response act as a container for the data sent back from api.
            IRestResponse response = client.Execute(request);
            //checking status code of response
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //deserializing content added in json file
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            //asserting for salary
            Assert.AreEqual(dataResponse.salary, "5550000");
        }
    }
}
