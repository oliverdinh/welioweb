using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaxWelio.Common.Exception;
using WaxWelio.Common.LangResource;
using WaxWelio.Common.Object;
using WaxWelio.Entities.Models;
using WaxWelio.Common.Enum;
using System.Net.Mail;

namespace WaxWelio.Common
{
    public static class Restful
    {
        private static ApiResponse _response;

        public static JObject Get(string url, ApiHeader apiHeader = null)
        {
            return CallApi(url, HttpMethod.Get, apiHeader, null);
        }

        public static JObject Post(string url, ApiHeader apiHeader, object data, bool hasArrayResult = false)
        {
            return CallApi(url, HttpMethod.Post, apiHeader, data);
        }

        public static JObject[] PostArray(string url, ApiHeader apiHeader, object data, bool hasArrayResult = false)
        {
            return CallApiArray(url, HttpMethod.Post, apiHeader, data);
        }

        public static JObject PostMultipartForm(string url, ApiHeader apiHeader, CreateClinicUserModel data)
        {
            return CallMultipartFormApi(url, apiHeader, data);
        }

        public static JObject PostMultipartForm(string url, ApiHeader apiHeader, UpdateClinicUserModel data)
        {
            return CallMultipartFormApi(url, apiHeader, data);
        }

        public static JObject PostMultipartFormClinic(string url, ApiHeader apiHeader, HospitalModel model)
        {
            return CallMultipartFormClinicApi(url, apiHeader, model);
        }

        public static JObject PostMultipartFormPatient(string url, ApiHeader apiHeader, UpdatePatientModel model)
        {
            return CallMultipartPatientFormApi(url, apiHeader, model);
        }

        public static ApiResponse GetApiException()
        {
            return _response;
        }

        public static T Get<T>(this JObject jObject, ApiKeyData key = null) where T : class
        {
            var result = key == null ? jObject : jObject[key.ToString()];

            if (result == null) return null;

            if (result is JObject)
            {
                return result.ToObject<T>();
            }
            if (result is JArray)
            {
                return result.ToObject<List<T>>().FirstOrDefault();
            }
            return null;
        }

        public static IList<T> GetList<T>(this JObject jObject, ApiKeyData key = null) where T : class
        {
            var result = key == null ? jObject : (jObject == null ? null : jObject[key.ToString()]);

            if (result == null) return new List<T>();

            if (result is JArray)
            {
                return result.ToObject<List<T>>();
            }

            return new List<T>();
        }

        public static IList<T> GetListArray<T>(this JObject[] jObject) where T : class
        {
            if (jObject == null || jObject.Count() == 0) return new List<T>();

            IList<T> list = new List<T>();
            foreach (var item in jObject)
            {
                list.Add(item.ToObject<T>());
            }
            return list;
        }

        private static JObject CallApi(string url, HttpMethod httpMethod, ApiHeader apiHeader, object data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var client = new HttpClient();
            var msg = new HttpRequestMessage(httpMethod, url);
            if (apiHeader != null && string.IsNullOrEmpty(apiHeader.Lang))
            {
                msg.Headers.Add("userId", apiHeader.UserId);
                msg.Headers.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                msg.Headers.Add("lang", apiHeader.Lang);
            }
            else
            {
                msg.Headers.Add("lang", "en");
            }
            if (!string.IsNullOrEmpty(apiHeader?.Token))
            {
                msg.Headers.Add("token", apiHeader.Token);
            }
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete)
            {
                var json = JsonConvert.SerializeObject(data);
                msg.Content = new StringContent(json);
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            
            var responseString = client.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

            if (!IsJsonString(responseString))
            {
                throw new ApiException(Resource.UnexpectedError);
            }

            var response = JsonConvert.DeserializeObject<ApiResponse>(responseString);

            if (!(response.ErrorMessage == "SUCCESS"))
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        private static JObject[] CallApiArray(string url, HttpMethod httpMethod, ApiHeader apiHeader, object data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var client = new HttpClient();
            var msg = new HttpRequestMessage(httpMethod, url);
            if (apiHeader != null && string.IsNullOrEmpty(apiHeader.Lang))
            {
                msg.Headers.Add("userId", apiHeader.UserId);
                msg.Headers.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                msg.Headers.Add("lang", apiHeader.Lang);
            }
            else
            {
                msg.Headers.Add("lang", "en");
            }
            if (!string.IsNullOrEmpty(apiHeader?.Token))
            {
                msg.Headers.Add("token", apiHeader.Token);
            }
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete)
            {
                var json = JsonConvert.SerializeObject(data);
                msg.Content = new StringContent(json);
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            var responseString = client.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

            if (!IsJsonString(responseString))
            {
                throw new ApiException(Resource.UnexpectedError);
            }

            var response = JsonConvert.DeserializeObject<ApiResponseArray>(responseString);

            if (!(response.ErrorMessage == "SUCCESS"))
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        private static JObject CallMultipartFormApi(string url, ApiHeader apiHeader, CreateClinicUserModel data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var httpClient = new HttpClient();
            if (apiHeader != null)
            {
                httpClient.DefaultRequestHeaders.Add("userId", apiHeader.UserId);
                httpClient.DefaultRequestHeaders.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                httpClient.DefaultRequestHeaders.Add("lang", apiHeader.Lang);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("lang", "en");
            }
            var multipart = new MultipartFormDataContent
            {
                {new StringContent(string.IsNullOrEmpty(data.FirstName) ? "" : data.FirstName), "FirstName"},
                {new StringContent(string.IsNullOrEmpty(data.LastName) ? "" : data.LastName), "LastName"},
                {new StringContent(string.IsNullOrEmpty(data.Email) ? "" : data.Email), "Email"},
                {new StringContent(string.IsNullOrEmpty(data.ClinicId) ? "" : data.ClinicId), "ClinicId"},
                {new StringContent(string.IsNullOrEmpty(data.ClinicName) ? "" : data.ClinicName), "ClinicName"},
                {new StringContent(string.IsNullOrEmpty(data.Title) ? "" : data.Title), "Title"},
                {new StringContent(string.IsNullOrEmpty(data.UserType.ToString()) ? "" : data.UserType.ToString()), "UserType"},
                {new StringContent(string.IsNullOrEmpty(data.AccessType.ToString()) ? "" : data.AccessType.ToString()), "AccessType"},
            };
            if (!string.IsNullOrEmpty(data.FileName))
            {
                multipart.Add(new ByteArrayContent(data.Images), "Image", data.FileName);
            }
            //if (!string.IsNullOrEmpty(data.Hash))
            //{
            //    multipart.Add(new StringContent(data.Hash), "hash");
            //}
            var result = httpClient.PostAsync(new Uri(url), multipart).Result;
            var response = JsonConvert.DeserializeObject<ApiResponse>(result.Content.ReadAsStringAsync().Result);
            if (!(response.ErrorMessage == "SUCCESS"))
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        private static JObject CallMultipartFormApi(string url, ApiHeader apiHeader, UpdateClinicUserModel data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var httpClient = new HttpClient();
            if (apiHeader != null)
            {
                httpClient.DefaultRequestHeaders.Add("userId", apiHeader.UserId);
                httpClient.DefaultRequestHeaders.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                httpClient.DefaultRequestHeaders.Add("lang", apiHeader.Lang);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("lang", "en");
            }
            var multipart = new MultipartFormDataContent
            {
                {new StringContent(string.IsNullOrEmpty(data.FirstName) ? "" : data.FirstName), "FirstName"},
                {new StringContent(string.IsNullOrEmpty(data.LastName) ? "" : data.LastName), "LastName"},
                {new StringContent(string.IsNullOrEmpty(data.Title) ? "" : data.Title), "Title"},
                {new StringContent(string.IsNullOrEmpty(data.DoctorId) ? "" : data.DoctorId), "DoctorId"},
                {new StringContent(string.IsNullOrEmpty(data.ClinicUserId) ? "" : data.ClinicUserId), "ClinicUserId"},
                {new StringContent(string.IsNullOrEmpty(data.UserType.ToString()) ? "" : data.UserType.ToString()), "UserType"},
                {new StringContent(string.IsNullOrEmpty(data.AccessType.ToString()) ? "" : data.AccessType.ToString()), "AccessType"},
            };
            if (!string.IsNullOrEmpty(data.FileName))
            {
                multipart.Add(new ByteArrayContent(data.Imagefile), "Image", data.FileName);
            }
            //if (!string.IsNullOrEmpty(data.Hash))
            //{
            //    multipart.Add(new StringContent(data.Hash), "hash");
            //}
            var result = httpClient.PostAsync(new Uri(url), multipart).Result;
            var response = JsonConvert.DeserializeObject<ApiResponse>(result.Content.ReadAsStringAsync().Result);
            if (!(response.ErrorMessage == "SUCCESS"))
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        private static JObject CallMultipartPatientFormApi(string url, ApiHeader apiHeader, UpdatePatientModel data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var httpClient = new HttpClient();
            if (apiHeader != null)
            {
                httpClient.DefaultRequestHeaders.Add("userId", apiHeader.UserId);
                httpClient.DefaultRequestHeaders.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                httpClient.DefaultRequestHeaders.Add("lang", apiHeader.Lang);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("lang", "en");
            }
            var multipart = new MultipartFormDataContent
            {
                {new StringContent(string.IsNullOrEmpty(data.PatientId) ? "" : data.PatientId), "id"},
                {new StringContent(string.IsNullOrEmpty(data.FirstName) ? "" : data.FirstName), "firstName"},
                {new StringContent(string.IsNullOrEmpty(data.LastName) ? "" : data.LastName), "lastName"},
                {new StringContent(string.IsNullOrEmpty(data.Email) ? "" : data.Email), "email"},
                {new StringContent(string.IsNullOrEmpty(data.Phone) ? "" : data.Phone), "phone"},
            };
            if (!string.IsNullOrEmpty(data.FileName))
            {
                multipart.Add(new ByteArrayContent(data.Image), "photo", data.FileName);
            }
            var result = httpClient.PostAsync(new Uri(url), multipart).Result;
            var response = JsonConvert.DeserializeObject<ApiResponse>(result.Content.ReadAsStringAsync().Result);
            if (!(response.ErrorMessage == "SUCCESS"))
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        private static JObject CallMultipartFormClinicApi(string url, ApiHeader apiHeader, HospitalModel data)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var httpClient = new HttpClient();
            if (apiHeader != null)
            {
                httpClient.DefaultRequestHeaders.Add("userId", apiHeader.UserId);
                httpClient.DefaultRequestHeaders.Add("sessionId", apiHeader.SessionId);
            }
            if (!string.IsNullOrEmpty(apiHeader?.Lang))
            {
                httpClient.DefaultRequestHeaders.Add("lang", apiHeader.Lang);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("lang", "en");
            }
            var multipart = new MultipartFormDataContent
            {
                {new StringContent(string.IsNullOrEmpty(data.Id) ? "" : data.Id), "id"},
                {new StringContent(string.IsNullOrEmpty(data.ClinicName) ? "" : data.ClinicName), "name"},
                {new StringContent(string.IsNullOrEmpty(data.AddressLine1) ? "" : data.AddressLine1), "address"},
                {new StringContent(string.IsNullOrEmpty(data.AddressLine2) ? "" : data.AddressLine2), "address2"},
                {new StringContent(string.IsNullOrEmpty(data.PhoneNumber[0]) ? "" : data.PhoneNumber[0]), "phones"},
                {new StringContent(string.IsNullOrEmpty(data.Suburb) ? "" : data.Suburb), "suburb"},
                {new StringContent(string.IsNullOrEmpty(data.PostCode) ? "" : data.PostCode), "postCode"},
                {new StringContent(string.IsNullOrEmpty(data.SignUpStatus.ToString()) ? "" : data.SignUpStatus.ToString()), "signUpStatus"},
            };
            //if (!string.IsNullOrEmpty(data.FileName))
            //{
            //    multipart.Add(new ByteArrayContent(data.imagefile), "file", data.FileName);
            //}
            
            var result = httpClient.PostAsync(new Uri(url), multipart).Result;
            var response = JsonConvert.DeserializeObject<ApiResponse>(result.Content.ReadAsStringAsync().Result);
            if (!response.Successed)
            {
                throw new ApiException(response.ErrorMessage, response.ErrorCode.ToString(), cultureCode.Name);
            }
            return response.Result;
        }

        //public static bool GetStatusResult(string url, HttpMethod httpMethod, ApiHeader apiHeader)
        //{
        //    var clientHandler = new WebRequestHandler();
        //    clientHandler.ClientCertificates.Add(new X509Certificate2());
        //    var client = new HttpClient();
        //    var msg = new HttpRequestMessage(httpMethod, url);

        //    if (apiHeader != null)
        //    {
        //        msg.Headers.Add("userId", apiHeader.UserId);
        //        msg.Headers.Add("sessionId", apiHeader.SessionId);
        //    }

        //    var responseString = client.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

        //    if (!IsJsonString(responseString))
        //    {
        //        throw new ApiException(Resource.UnexpectedError);
        //    }

        //    var response = JsonConvert.DeserializeObject<ApiResponse>(responseString);

        //    _response = response;
        //    return response.IsSuccessful;
        //}

        private static bool IsJsonString(string jsonString)
        {
            try
            {
                JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (System.Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        public static Api365UserInfoResponse getEmail365(string token)
        {
            var cultureCode = Thread.CurrentThread.CurrentCulture;
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(new X509Certificate2());
            var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/");

            msg.Headers.Add("Authorization", "Bearer " + token);

            var responseString = client.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

            if (!IsJsonString(responseString))
            {
                throw new ApiException(Resource.UnexpectedError);
            }

            var response = JsonConvert.DeserializeObject<Api365UserInfoResponse>(responseString);

            return response;
        }

        public static Api365UserInfoResponse CreateUserOffice365(string email, string displayName, string password, string token)
        {
            if (token != null)
            {
                var cultureCode = Thread.CurrentThread.CurrentCulture;
                var clientHandler = new WebRequestHandler();
                clientHandler.ClientCertificates.Add(new X509Certificate2());
                var client = new HttpClient();
                var msg = new HttpRequestMessage(HttpMethod.Post, "https://graph.microsoft.com/v1.0/users/");

                msg.Headers.Add("Authorization", "Bearer " + token);
                MailAddress addr = new MailAddress(email);
                var data = new Api365CreateUser
                {
                    AccountEnabled = true,
                    DisplayName = displayName,
                    MailNickname = addr.User,
                    UserPrincipalName = email,
                    PasswordProfile = new ApiPassword365Profile
                    {
                        ForceChangePasswordNextSignIn = true,
                        Password = password
                    }
                };
                var json = JsonConvert.SerializeObject(data);
                msg.Content = new StringContent(json);
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseString = client.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

                if (!IsJsonString(responseString))
                {
                    throw new ApiException(Resource.UnexpectedError);
                }

                var response = JsonConvert.DeserializeObject<Api365UserInfoResponse>(responseString);

                return response;
            }
            else return null;
        }
    }
}