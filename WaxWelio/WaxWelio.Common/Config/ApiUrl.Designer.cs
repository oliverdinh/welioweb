﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WaxWelio.Common.Config {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    public sealed partial class ApiUrl : global::System.Configuration.ApplicationSettingsBase {
        
        private static ApiUrl defaultInstance = ((ApiUrl)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ApiUrl())));
        
        public static ApiUrl Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://welioapi-staging.azurewebsites.net/api/")]
        public string RootApi {
            get {
                return ((string)(this["RootApi"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/CreateClinicUser")]
        public string SignUp {
            get {
                return ((string)(this["SignUp"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/GetListClinic")]
        public string GetListClinic
        {
            get
            {
                return ((string)(this["GetListClinic"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/{0}/approve")]
        public string ApproveClinic {
            get {
                return ((string)(this["ApproveClinic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://welio.clas.mobi:8445/staging-welio/login")]
        public string SystemLogin {
            get {
                return ((string)(this["SystemLogin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospitals?status={0}&signUpStatus={1}&start={2}&length={3}&getAdminUsers={4}&sear" +
            "chExactly={5}&search={6}")]
        public string ListHospital {
            get {
                return ((string)(this["ListHospital"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("doctor/appointments?hospitalId={0}&doctorId={1}&status={2}&startDate={3}&endDate=" +
            "{4}&start={5}&length={6}&search={7}&searchExactly={8}&sortField={9}&order={10}")]
        public string ListAppointment {
            get {
                return ((string)(this["ListAppointment"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Patients/GetPatientByPhone")]
        public string GetPatientByPhone {
            get {
                return ((string)(this["GetPatientByPhone"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TrackingCallTimes/GetListPricingOfClinic/{0}")]
        public string ListPricing {
            get {
                return ((string)(this["ListPricing"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/price/save")]
        public string AddUpdatePrice {
            get {
                return ((string)(this["AddUpdatePrice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/GetAllClinicUser")]
        public string ListUser {
            get {
                return ((string)(this["ListUser"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/GetUserOfClinic")]
        public string ListClinicUser
        {
            get
            {
                return ((string)(this["ListClinicUser"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/CreateAppointment")]
        public string CreateNormalingAppointment {
            get {
                return ((string)(this["CreateNormalingAppointment"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("doctor/appointment/doFloating")]
        public string CreateFloatingAppointment {
            get {
                return ((string)(this["CreateFloatingAppointment"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("appointment/{0}/get")]
        public string GetAppointmentById {
            get {
                return ((string)(this["GetAppointmentById"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("doctor/appointment/do")]
        public string AddAppointmentNormal {
            get {
                return ((string)(this["AddAppointmentNormal"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/user/active?hash={0}&email={1}&login=true")]
        public string ActiveClinic {
            get {
                return ((string)(this["ActiveClinic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("changePassword")]
        public string ChangePassword {
            get {
                return ((string)(this["ChangePassword"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/user/verifyActiveUrl?hash={0}&email={1}&login=true")]
        public string CheckExpired {
            get {
                return ((string)(this["CheckExpired"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/doctor/resendActiveUrl?email={0}")]
        public string ResendActiveEmail {
            get {
                return ((string)(this["ResendActiveEmail"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/user/addOrEdit")]
        public string AddOrUpdateUser {
            get {
                return ((string)(this["AddOrUpdateUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Patients/GetPatientDetails/{0}")]
        public string GetUserProfile {
            get {
                return ((string)(this["GetUserProfile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/user/confirmInvitationUrl")]
        public string ConfirmInvitationNewUser {
            get {
                return ((string)(this["ConfirmInvitationNewUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/EditPiricing")]
        public string AddPrice {
            get {
                return ((string)(this["AddPrice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/{0}/get")]
        public string GetClinic {
            get {
                return ((string)(this["GetClinic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("appointment/{0}/pushCalling")]
        public string PushNotifiCall {
            get {
                return ((string)(this["PushNotifiCall"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/CreateOrEditPayment")]
        public string AddOrEditBankAccount {
            get {
                return ((string)(this["AddOrEditBankAccount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/GetPaymentOfClinic/{0}")]
        public string GetBankAccount {
            get {
                return ((string)(this["GetBankAccount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/TellMyClinic")]
        public string NotifyClinic {
            get {
                return ((string)(this["NotifyClinic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/EditClinicUser")]
        public string EditClinicUser
        {
            get {
                return ((string)(this["EditClinicUser"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/AddClinicUser")]
        public string CreateClinicUser
        {
            get
            {
                return ((string)(this["CreateClinicUser"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/doctor/requestPassword?id={0}")]
        public string ForgotPassword {
            get {
                return ((string)(this["ForgotPassword"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("setPasswordForSignUp")]
        public string SetPasswordForSignUp {
            get {
                return ((string)(this["SetPasswordForSignUp"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/ChangeStatusAppointment")]
        public string UpateAppointmentStatus {
            get {
                return ((string)(this["UpateAppointmentStatus"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/CancelAppointmentByClinic")]
        public string CancelAppointment
        {
            get
            {
                return ((string)(this["CancelAppointment"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/save")]
        public string EditClinic {
            get {
                return ((string)(this["EditClinic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/user/{0}/close?hospitalId={1}")]
        public string CloseUnconfirmAccount {
            get {
                return ((string)(this["CloseUnconfirmAccount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/EditAppointment")]
        public string UpdateAppointment {
            get {
                return ((string)(this["UpdateAppointment"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/user/{0}/reopen?hospitalId={1}")]
        public string ReOpenAccount {
            get {
                return ((string)(this["ReOpenAccount"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/CheckDoctorExists")]
        public string CheckDoctorExists
        {
            get
            {
                return ((string)(this["CheckDoctorExists"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Patients/GetAllPatients")]
        public string ListPatient {
            get {
                return ((string)(this["ListPatient"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Patients/EditPatient")]
        public string UpdatePatient {
            get {
                return ((string)(this["UpdatePatient"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hospital/{0}/get")]
        public string GetClinicInforForPartner {
            get {
                return ((string)(this["GetClinicInforForPartner"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("public/user/confirmInvitationUrl?hash={0}&email={1}&login=true")]
        public string ConfirmInvitationUser {
            get {
                return ((string)(this["ConfirmInvitationUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/ReInvatieAcount")]
        public string ResendInviteEmail {
            get {
                return ((string)(this["ResendInviteEmail"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("appointment/call/{0}/ping?status={1}")]
        public string CallStatus {
            get {
                return ((string)(this["CallStatus"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/LoginDoctor")]
        public string LoginDoctor
        {
            get
            {
                return ((string)(this["LoginDoctor"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/GetClinicDetails/{0}")]
        public string GetClinicDetails
        {
            get
            {
                return ((string)(this["GetClinicDetails"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Clinics/EditClinic")]
        public string UpdateClinic
        {
            get
            {
                return ((string)(this["UpdateClinic"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/GetListAppointmentsOfDoctor")]
        public string GetListAppointmentsOfDoctor
        {
            get
            {
                return ((string)(this["GetListAppointmentsOfDoctor"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/SelectClinic/{0}")]
        public string GetDoctorsByClinic
        {
            get
            {
                return ((string)(this["GetDoctorsByClinic"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/GetClinicUserDetails/{0}")]
        public string GetUserDetails
        {
            get
            {
                return ((string)(this["GetUserDetails"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ClinicUsers/ChangeStatusUserClinic")]
        public string ChangeStatusUserClinic
        {
            get
            {
                return ((string)(this["ChangeStatusUserClinic"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Appointments/CheckUpcomingAppointmentsOfDoctor")]
        public string CheckUpcomingAppointmentsOfDoctor
        {
            get
            {
                return ((string)(this["CheckUpcomingAppointmentsOfDoctor"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/SendOffice365ToUser")]
        public string SendOffice365ToUser
        {
            get
            {
                return ((string)(this["SendOffice365ToUser"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/UpdateAccountOffice365")]
        public string UpdateAccountOffice365
        {
            get
            {
                return ((string)(this["UpdateAccountOffice365"]));
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doctors/ActiveDoctor")]
        public string ActiveDoctor
        {
            get
            {
                return ((string)(this["ActiveDoctor"]));
            }
        }
    }
}
