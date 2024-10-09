using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;

using System.Globalization;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;
using Stimulsoft.Report;
//using System.Telerik.Web.UI;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BusinessTier
{
    public BusinessTier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable g_ErrorMessagesDataTable;

    public static SqlConnection getConnection()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        return conn;
    }

    public static string getConnection1()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        return conString;
    }

    public static void DisposeConnection(SqlConnection conn)
    {
        conn.Close();
        conn.Dispose();
    }

    public static void DisposeReader(SqlDataReader reader)
    {
        reader.Close();
        reader.Dispose();
    }

    public static void DisposeAdapter(SqlDataAdapter adapter)
    {
        adapter.Dispose();
    }

    public static SqlDataReader VaildateUserLogin(SqlConnection connec, string Logind, string Password)
    {
        SqlCommand cmd = new SqlCommand("sp_Validate_UserLogin", connec);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Useridp", Logind);
        cmd.Parameters.AddWithValue("@Passp", Password);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    //--------------------------< Function For Master Platform />-----------------------------------

    public static SqlDataReader getPlatformInfoById(SqlConnection getPlatformconn, string platformid)
    {
        int delval = 0;
        string sql = "select * FROM MasterPlatform WHERE PlatformId='" + platformid + "' and  Deleted='" + delval + "' ORDER BY PlatformName";
        SqlCommand cmd = new SqlCommand(sql, getPlatformconn);
        SqlDataReader getPlatformreader = cmd.ExecuteReader();
        return getPlatformreader;
    }

    public static int DeletePlatformGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterPlatform_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@platformidp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static int SavePlatformMaster(SqlConnection conn, string strName, string strDesc, string strUserID, string saveFlg, string strId)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveFlg.ToString() == "N")
        {
            sp_Name = "sp_MasterPlatform_Insert";
        }
        else
        {
            sp_Name = "sp_MasterPlatform_Update";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveFlg.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", strId);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }

        dCmd.Parameters.AddWithValue("@namep ", strName);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkPlatformName(SqlConnection connCheck, string platformname)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterPlatform_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@platformnamep", platformname);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader gePlatformIdByPlatformName(SqlConnection connGetId, string platformName)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterPlatform_GetIDByName]", connGetId);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@platformnamep", platformName);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    //------------------------------------------------------------------------------------------------
    //--------------------------< Function For Master Check IS Duplicate >--------------------------------------

    public static SqlDataReader IDCheck(SqlConnection con, string ID, string Flag)
    {
        SqlCommand cmd = new SqlCommand("sp_Master_IsDuplicate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@Flag", Flag);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveStaffMasterOffical(SqlConnection conn, int StaffOfficial_ID, int STAFF_ID, string DOJ, string EMPType, string Designation, string POSITION, int COUNTRY_ID, int DEPT_ID, int Company_ID, int ReportingTo, string OGSPNO, string OGSPVALIDITY, string MEDICALVALIDITY, string H2SCERTVALIDITY, string BOSIETVALIDITY, string COMPENTANCYCARDVALIDITY, string SHELLPASSPORTVALIDITY, string Skill, string CIDB, string ConfineSpace, string WorkPermit, string BordingPass, string PTW, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Staff", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StaffOfficial_ID", StaffOfficial_ID);
        dCmd.Parameters.AddWithValue("@STAFF_ID", STAFF_ID);
        dCmd.Parameters.AddWithValue("@DOJ", DOJ);
        dCmd.Parameters.AddWithValue("@EMPType", EMPType);
        dCmd.Parameters.AddWithValue("@Designation", Designation);
        dCmd.Parameters.AddWithValue("@POSITION", POSITION);
        dCmd.Parameters.AddWithValue("@COUNTRY_ID", COUNTRY_ID);
        dCmd.Parameters.AddWithValue("@DEPT_ID", DEPT_ID);
        dCmd.Parameters.AddWithValue("@Company_ID", Company_ID);
        dCmd.Parameters.AddWithValue("@ReportingTo", ReportingTo);
        dCmd.Parameters.AddWithValue("@OGSPNO", OGSPNO);
        dCmd.Parameters.AddWithValue("@OGSPVALIDITY", OGSPVALIDITY);
        dCmd.Parameters.AddWithValue("@MEDICALVALIDITY", MEDICALVALIDITY);
        dCmd.Parameters.AddWithValue("@H2SCERTVALIDITY", H2SCERTVALIDITY);
        dCmd.Parameters.AddWithValue("@BOSIETVALIDITY", BOSIETVALIDITY);
        dCmd.Parameters.AddWithValue("@COMPENTANCYCARDVALIDITY", COMPENTANCYCARDVALIDITY);
        dCmd.Parameters.AddWithValue("@SHELLPASSPORTVALIDITY", SHELLPASSPORTVALIDITY);
        dCmd.Parameters.AddWithValue("@Skill", Skill);
        dCmd.Parameters.AddWithValue("@CIDB", CIDB);
        dCmd.Parameters.AddWithValue("@ConfineSpace", ConfineSpace);
        dCmd.Parameters.AddWithValue("@WorkPermit", WorkPermit);
        dCmd.Parameters.AddWithValue("@BordingPass", BordingPass);
        dCmd.Parameters.AddWithValue("@PTW", PTW);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For Master Department >--------------------------------------

    public static int SavePMTMaster(SqlConnection conn, int PMT_ID, int STAFF_ID, string strUserID, string saveFlg)
    {
        SqlCommand dCmd = new SqlCommand("sp_PMT", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@PMT_ID", PMT_ID);
        dCmd.Parameters.AddWithValue("@STAFF_ID", STAFF_ID);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();
    }

    //--------------------------< Function For Master Department >--------------------------------------

    public static int SaveDepartmentMaster(SqlConnection conn, int intid, string strcode, string strName, bool intlab, string strshortname, string strDesc, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Department", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@DeptIdp", intid);
        dCmd.Parameters.AddWithValue("@Deptcodep", strcode);
        dCmd.Parameters.AddWithValue("@Deptnamep", strName);
        dCmd.Parameters.AddWithValue("@labp", intlab);
        dCmd.Parameters.AddWithValue("@shortnameP", strshortname);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For Master Staff >--------------------------------------

    public static int SaveStaffMaster(SqlConnection conn, int intstaffid, string strstaffNo, string strStaffName, string DateOfBirth, string Nationality, string strcontactno, string Contact_no2, string stremail, string ICNO, string PassportNo, string PassportExpiry, string VisaEntry, string VisaExpiry, string PermanentAddress, string TemporaryAddress, string Photo, string Resume, string Certificate, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Staff_Personal", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StaffIdp", intstaffid);
        dCmd.Parameters.AddWithValue("@StaffNOp", strstaffNo);
        dCmd.Parameters.AddWithValue("@Staffnamep", strStaffName);
        dCmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
        dCmd.Parameters.AddWithValue("@Nationality", Nationality);
        dCmd.Parameters.AddWithValue("@contactnop", strcontactno);
        dCmd.Parameters.AddWithValue("@contactnop2", Contact_no2);
        dCmd.Parameters.AddWithValue("@emailp", stremail);
        dCmd.Parameters.AddWithValue("@ICNO", ICNO);
        dCmd.Parameters.AddWithValue("@PassportNo", PassportNo);
        dCmd.Parameters.AddWithValue("@PassportExpiry", PassportExpiry);
        dCmd.Parameters.AddWithValue("@VisaEntry", VisaEntry);
        dCmd.Parameters.AddWithValue("@VisaExpiry", VisaExpiry);
        dCmd.Parameters.AddWithValue("@PermanentAddress", PermanentAddress);
        dCmd.Parameters.AddWithValue("@TemporaryAddress", TemporaryAddress);
        dCmd.Parameters.AddWithValue("@Photo", Photo);
        dCmd.Parameters.AddWithValue("@Resume", Resume);
        dCmd.Parameters.AddWithValue("@Certificate", Certificate);

        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------

    //--------------------------< Function For Master Country >--------------------------------------

    public static int SaveCountryMaster(SqlConnection conn, int intcountryid, string strcountryName, string strRemarks, string Location, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Country", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CountryIdp", intcountryid);
        dCmd.Parameters.AddWithValue("@Countrynamp", strcountryName);
        dCmd.Parameters.AddWithValue("@CountryCode", strRemarks);
        dCmd.Parameters.AddWithValue("@Location", Location);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Function For Master State >--------------------------------------

    public static int SaveStateMaster(SqlConnection conn, int intstateid, string strStateName, string strRemarks, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_State", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@StateIdp", intstateid);
        dCmd.Parameters.AddWithValue("@Statenamp", strStateName);
        dCmd.Parameters.AddWithValue("@Remarksp", strRemarks);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For AssignStaff >--------------------------------------

    public static int SaveAssignStaff(SqlConnection conn, int Assign_ID, string Project_ID, string Staff_ID, string Project_Contact_id, string AssignStartDt, string AssignEndtDt, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("[sp_AssignStaff]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Assign_ID", Assign_ID);
        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@Staff_ID", Staff_ID);
        dCmd.Parameters.AddWithValue("@Project_Contact_id", Project_Contact_id);
        dCmd.Parameters.AddWithValue("@AssignStartDt", AssignStartDt);
        dCmd.Parameters.AddWithValue("@AssignEndtDt", AssignEndtDt);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For FreeLancer >--------------------------------------

    public static int SaveFreeLancer(SqlConnection conn, int FreeLancer_ID, int Project_ID, int Staff_ID, int Project_Contact_id, string Staff_Name, string ICNo, double Salary, string BasisSalary, string FileName, string AssignStartDt, string AssignEndtDt, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("[sp_FreeLancer]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@FreeLancer_ID", FreeLancer_ID);
        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@Staff_ID", Staff_ID);
        dCmd.Parameters.AddWithValue("@Project_Contact_id", Project_Contact_id);
        dCmd.Parameters.AddWithValue("@Staff_Name", Staff_Name);
        dCmd.Parameters.AddWithValue("@ICNo", ICNo);
        dCmd.Parameters.AddWithValue("@Salary", Salary);
        dCmd.Parameters.AddWithValue("@BasisSalary", BasisSalary);
        dCmd.Parameters.AddWithValue("@FileName", FileName);
        dCmd.Parameters.AddWithValue("@AssignStartDt", AssignStartDt);
        dCmd.Parameters.AddWithValue("@AssignEndtDt", AssignEndtDt);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //-------------------------------------Function For Master Project--------------------------------------------------------

    public static int SaveProjectMaster(SqlConnection conn, int Project_ID, string Project_No, string Project_Name, string Description, string SupplyScope, string strSupplyscopeothers, DateTime StartDate, DateTime EndDate, double TenderValue, string TenderCurrency, DateTime ProjectAwardDate, int CUSTOMER_ID, double Bank, string BankCurrency, double Insurance, string InsuranceCurrency, int STAFF_ID, string FileName, int Project_NameID, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Project", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@Project_No", Project_No);
        dCmd.Parameters.AddWithValue("@Project_Name", Project_Name);
        dCmd.Parameters.AddWithValue("@Description", Description);
        dCmd.Parameters.AddWithValue("@SupplyScope", SupplyScope);
        dCmd.Parameters.AddWithValue("@SupplyScopeOthers", strSupplyscopeothers);

        dCmd.Parameters.AddWithValue("@StartDate", StartDate);
        dCmd.Parameters.AddWithValue("@EndDate", EndDate);
        dCmd.Parameters.AddWithValue("@TenderValue", TenderValue);
        dCmd.Parameters.AddWithValue("@TenderCurrency", TenderCurrency);
        dCmd.Parameters.AddWithValue("@ProjectAwardDate", ProjectAwardDate);
        dCmd.Parameters.AddWithValue("@CUSTOMER_ID", CUSTOMER_ID);
        dCmd.Parameters.AddWithValue("@Bank", Bank);
        dCmd.Parameters.AddWithValue("@BankCurrency", BankCurrency);
        dCmd.Parameters.AddWithValue("@Insurance", Insurance);
        dCmd.Parameters.AddWithValue("@InsuranceCurrency", InsuranceCurrency);
        dCmd.Parameters.AddWithValue("@STAFF_ID", STAFF_ID);
        dCmd.Parameters.AddWithValue("@FileName", FileName);
        dCmd.Parameters.AddWithValue("@Project_NameID", Project_NameID);

        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //-------------------------------------Function For Project Po--------------------------------------------------------

    public static int SaveProjectPo(SqlConnection conn, int ProjectPo_ID, string Project_ID, DateTime StartDate, DateTime EndDate, double TenderValue, string TenderCurrency, string SupplyScope, string SupplyScopeOthers, DateTime ProjectAwardDate, string Description, int STAFF_ID, int CUSTOMER_ID, int COUNTRY_ID, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_ProjectPo", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ProjectPo_ID", ProjectPo_ID);
        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@StartDate", StartDate);
        dCmd.Parameters.AddWithValue("@EndDate", EndDate);
        dCmd.Parameters.AddWithValue("@TenderValue", TenderValue);
        dCmd.Parameters.AddWithValue("@TenderCurrency", TenderCurrency);
        dCmd.Parameters.AddWithValue("@SupplyScope", SupplyScope);
        dCmd.Parameters.AddWithValue("@SupplyScope_Others", SupplyScopeOthers);
        dCmd.Parameters.AddWithValue("@ProjectAwardDate", ProjectAwardDate);
        dCmd.Parameters.AddWithValue("@Description", Description);
        dCmd.Parameters.AddWithValue("@STAFF_ID", STAFF_ID);
        dCmd.Parameters.AddWithValue("@CUSTOMER_ID", CUSTOMER_ID);
        dCmd.Parameters.AddWithValue("@COUNTRY_ID", COUNTRY_ID);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }
    //-------------------------------------

    public static int SaveProjectName(SqlConnection conn, int Project_Nameid, string Project_Name, int CUSTOMER_ID, string ContractDuration, string strUserID, string saveFlg)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Master_Project_Name]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Project_Nameid", Project_Nameid);
        dCmd.Parameters.AddWithValue("@Project_Name", Project_Name);
        dCmd.Parameters.AddWithValue("@CUSTOMER_ID", CUSTOMER_ID);
        dCmd.Parameters.AddWithValue("@ContractDuration", ContractDuration);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //-------------------------------------Function For Master Project Contact--------------------------------------------------------

    public static int SaveProjectMasterContact(SqlConnection conn, int Project_Contact_id, int Project_ID, string Designation, int Manpower, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("[sp_Master_Project_Contact]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Project_Contact_id", Project_Contact_id);
        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@Designation", Designation);
        dCmd.Parameters.AddWithValue("@Manpower", Manpower);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //-------------------------------------Function For Master Project Update--------------------------------------------------------

    public static int SaveProjectMasterUpdate(SqlConnection conn, int Project_ID, int CustCont_ID1, int CustCont_ID2, int CustCont_ID3, string FileName, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("[sp_Master_Project_Update]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        dCmd.Parameters.AddWithValue("@CustCont_ID1", CustCont_ID1);
        dCmd.Parameters.AddWithValue("@CustCont_ID2", CustCont_ID2);
        dCmd.Parameters.AddWithValue("@CustCont_ID3", CustCont_ID3);
        dCmd.Parameters.AddWithValue("@FileName", FileName);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //-------------------------------------Function For Master Company--------------------------------------------------------

    public static int SaveCompanyMaster(SqlConnection conn, int Company_ID, string Company_Name, string Company_CODE, string REG_ROC_NO, string REG_DATE, string ADDR_LINE1, string ADDR_LINE2, string ADDR_LINE3, string CITY, string STATE, string COUNTRY, string POSTAL_CODE, string CONTACT_NO, string FAX_NO, string EMAIL, string WEBSITE, int COUNTRY_ID, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Company", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Company_ID", Company_ID);
        dCmd.Parameters.AddWithValue("@Company_Name", Company_Name);
        dCmd.Parameters.AddWithValue("@Company_CODE", Company_CODE);
        dCmd.Parameters.AddWithValue("@REG_ROC_NO", REG_ROC_NO);
        dCmd.Parameters.AddWithValue("@REG_DATE", REG_DATE);

        dCmd.Parameters.AddWithValue("@ADDR_LINE1", ADDR_LINE1);
        dCmd.Parameters.AddWithValue("@ADDR_LINE2", ADDR_LINE2);
        dCmd.Parameters.AddWithValue("@ADDR_LINE3", ADDR_LINE3);
        dCmd.Parameters.AddWithValue("@CITY", CITY);
        dCmd.Parameters.AddWithValue("@STATE", STATE);
        dCmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);
        dCmd.Parameters.AddWithValue("@POSTAL_CODE", POSTAL_CODE);
        dCmd.Parameters.AddWithValue("@CONTACT_NO", CONTACT_NO);
        dCmd.Parameters.AddWithValue("@FAX_NO", FAX_NO);
        dCmd.Parameters.AddWithValue("@EMAIL", EMAIL);
        dCmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);
        dCmd.Parameters.AddWithValue("@COUNTRY_ID", COUNTRY_ID);

        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For Master Customer >--------------------------------------

    public static int SaveCustomerMaster(SqlConnection conn, int intid, string strcrmid, string strcustcode, string strcustName, string strRecno, string Addrline1, string Addrline2, string strstate, string strcountry, int intpostcode, string strcontactno, string strfaxno, string stremail, string strwebsite, string strcontrctno, bool isbranch, bool iscontract, int intExcompldays, int intpercn, int intAcntmngr, DateTime dtregdate, string straddrs3, string strcity, string strphonecode, string strphonecode1, string strphone1, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Customer", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@CustomerIdp", intid);
        dCmd.Parameters.AddWithValue("@CRMIDp", strcrmid);
        dCmd.Parameters.AddWithValue("@Customercodep", strcustcode);
        dCmd.Parameters.AddWithValue("@Customernamep", strcustName);
        dCmd.Parameters.AddWithValue("@CustomerRecNop", strRecno);
        dCmd.Parameters.AddWithValue("@address1p", Addrline1);
        dCmd.Parameters.AddWithValue("@address2p", Addrline2);
        dCmd.Parameters.AddWithValue("@statep", strstate);
        dCmd.Parameters.AddWithValue("@countryp", strcountry);
        dCmd.Parameters.AddWithValue("@Postalcodep", intpostcode);
        dCmd.Parameters.AddWithValue("@contactnop", strcontactno);
        dCmd.Parameters.AddWithValue("@faxnop", strfaxno);
        dCmd.Parameters.AddWithValue("@emailp", stremail);
        dCmd.Parameters.AddWithValue("@Websitep", strwebsite);
        dCmd.Parameters.AddWithValue("@Contractnop", strcontrctno);
        dCmd.Parameters.AddWithValue("@IsBranchp", isbranch);
        dCmd.Parameters.AddWithValue("@IsContractp", iscontract);
        dCmd.Parameters.AddWithValue("@Excompldays", intExcompldays);
        dCmd.Parameters.AddWithValue("@percentagep", intpercn);
        dCmd.Parameters.AddWithValue("@AcntMngrp", intAcntmngr);

        dCmd.Parameters.AddWithValue("@regdatep", dtregdate);
        dCmd.Parameters.AddWithValue("@address3p", straddrs3);
        dCmd.Parameters.AddWithValue("@cityp", strcity);
        dCmd.Parameters.AddWithValue("@phonecodep", strphonecode);
        dCmd.Parameters.AddWithValue("@phonecode1p", strphonecode1);
        dCmd.Parameters.AddWithValue("@phone1p", strphone1);

        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Function For Master Contact >--------------------------------------

    public static int SaveContactMaster(SqlConnection conn, int intconatctid, int intcustomerid, string strContactName, string strcontactno, string stremailid, string strDepartment, string strUserID, string saveFlg)
    {

        SqlCommand dCmd = new SqlCommand("sp_Master_Contact", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@ContatcIdp", intconatctid);
        dCmd.Parameters.AddWithValue("@CustomerIdp", intcustomerid);
        dCmd.Parameters.AddWithValue("@ContactNamep", strContactName);
        dCmd.Parameters.AddWithValue("@ContactNop", strcontactno);
        dCmd.Parameters.AddWithValue("@Departmentp", strDepartment);
        dCmd.Parameters.AddWithValue("@Emailp", stremailid);
        dCmd.Parameters.AddWithValue("@useridp", strUserID);
        dCmd.Parameters.AddWithValue("@flagp", saveFlg);
        return dCmd.ExecuteNonQuery();

    }

    //--------------------------< Function For Master Module >--------------------------------------

    public static SqlDataReader getMasterModule(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM Menulist ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModuleById(SqlConnection connect, string strModuleId)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' and ModuleId='" + strModuleId + "' ORDER BY ModuleId";
        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;

    }

    public static int DeleteModuleGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterModule_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@moduleidp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkModuleName(SqlConnection connCheck, string name)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterModule_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@modulenamep", name);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveModuleMaster(SqlConnection conn, string name, string desc, string appflag, string userid, string saveflag, string modid)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveflag.ToString() == "N")
        {
            sp_Name = "[sp_MasterModule_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterModule_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", modid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        dCmd.Parameters.AddWithValue("@namep", name);
        dCmd.Parameters.AddWithValue("@descriptionp", desc);
        dCmd.Parameters.AddWithValue("@approvalflag", appflag);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();
    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Function For Master User >--------------------------------------

    public static SqlDataReader getMasterUserInfo(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE Deleted='" + delval + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterUserByID(SqlConnection conn, string strID)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE ID='" + strID + "' and  Deleted='" + delval + "' ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getUserNameByID(SqlConnection conn, string strID)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_getUserName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@idp", strID);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static string getMasterUserIDByName(SqlConnection conn, string strName)
    {
        int delval = 0;
        string sql = "select ID FROM Vw_MasterUser_Staff WHERE UserName like '%" + strName + "%' and  Deleted='" + delval + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        string ret = reader[0].ToString();
        BusinessTier.DisposeReader(reader);
        //BusinessTier.DisposeConnection(conn);
        return ret;
    }

    public static SqlDataReader getMasterUserByLoginId(SqlConnection conn, string strLoginId)
    {
        int delval = 0;
        string sql = "select * FROM Vw_MasterUser_Staff WHERE Deleted='" + delval + "' and LoginId='" + strLoginId + "'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModulePermisnByUserId(SqlConnection connModulePermission, string strUserId)
    {
        int delval = 0;
        string sql = "select * FROM vw_MasterModulePermission_MasterModuleByModuleID WHERE Deleted='" + delval + "' and UserId='" + strUserId.ToString() + "' order by modulename";
        SqlCommand cmd = new SqlCommand(sql, connModulePermission);
        SqlDataReader readerModulePermission = cmd.ExecuteReader();
        return readerModulePermission;
    }

    public static int DeleteUserGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUser_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@masteruseridp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUserMaster(SqlConnection connSave, int intstaffid, string strloginid, string strpass, string strCreatedByID, string strSaveFlag, string strCurrUserId)
    {
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_MasterUser_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterUser_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@idp", strCurrUserId);
        }
        dCmd.Parameters.AddWithValue("@loginidp", strloginid);
        dCmd.Parameters.AddWithValue("@passp", strpass);
        dCmd.Parameters.AddWithValue("@Staffidp", intstaffid);
        // dCmd.Parameters.AddWithValue("@isapprovalrqrdp", strapprqrd);
        //  dCmd.Parameters.AddWithValue("@isnotifyrqrd", strnotifyrqrd);
        dCmd.Parameters.AddWithValue("@useridp", strCreatedByID);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader checkUserLoginId(SqlConnection connCheck, string strLoginId)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_IsDuplicate]", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@loginidp", strLoginId);
        //cmd.Parameters.AddWithValue("@Flag", Flag);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader checkUserApprovalByUserId(SqlConnection connectUserAprvl, long lnguserid)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterUserApproval_CheckUserId", connectUserAprvl);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@useridp", lnguserid);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveUserMasterApproval(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserApproval_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@approvalp", intlinebyline);
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUserMasterModulePermission(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserModulePermission_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@moduleidp", intlinebyline);
        // dCmd.Parameters.AddWithValue("@appflag", "Y");
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getPlatformInfo_ByUserID(SqlConnection connGetID, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterPlatform_GetAllByUserId", connGetID);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@UserIDp", id);
        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMail_ByPlatformID(SqlConnection conn, string platformid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUser_getEmailByPlatformID]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Platformidp", platformid);
        SqlDataReader reader = dCmd.ExecuteReader();
        return reader;
    }

    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------

    public static void BindErrorMessageDetails(SqlConnection connError)
    {
        string sql = "select * FROM MasterErrorMessage order by OrderNo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connError);
        g_ErrorMessagesDataTable = new DataTable();
        sqlDataAdapter.Fill(g_ErrorMessagesDataTable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
    }

    public static void InsertLogAuditTrial(SqlConnection connLog, string userid, string module, string activity, string result, string flag)
    {
        string sp_Name;
        if (flag.ToString() == "Log")
        {
            sp_Name = "[sp_Master_Insert_Log]";
        }
        else
        {
            sp_Name = "[sp_Master_Insert_AuditTrail]";
        }

        SqlCommand dCmd = new SqlCommand(sp_Name, connLog);

        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@modulep", module);
        dCmd.Parameters.AddWithValue("@activityp", activity);
        dCmd.Parameters.AddWithValue("@resultp", result);
        dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getMenuList(SqlConnection conn)
    {
        string sql = "select Category, seqCategory from MenuList group by Category,seqCategory order by seqCategory";

        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMenuList(SqlConnection conn, string strUserId, string usertype)
    {
        string sql = "";
        //  if (usertype.ToString().Trim() == "admin")
        sql = "select Category, seqCategory FROM MenuList group by Category,seqCategory order by seqCategory";
        //  else
        //   sql = "select Category, seqCategory from vw_MenuList_MasterModulePermission  where UserId='" + strUserId + "' group by Category,seqCategory order by seqCategory";

        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static DataTable getSubMenuItems(string category, string uid, string usertype)
    {
        DataTable ret = new DataTable();
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "";
        //if (usertype.ToString().Trim() == "admin")
        //{
        //    sql = "select ModuleID, Href, Menulist FROM MenuList where Category = '" + category + "' order by seqMenu";
        //}
        //else
        //{
        sql = "select ModuleID, Href, Menulist FROM vw_MenuList_MasterModulePermission where Category = '" + category + "' and staff_id='" + uid + "' and Deleted=0 order by seqMenu";
        //}
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        ret.Load(reader);
        BusinessTier.DisposeConnection(conn);
        return ret;
    }

    //-----------------------Manual Function--------------------------

    public static string chkdate(string strdate)
    {
        string dt = string.Empty;
        if (string.IsNullOrEmpty(strdate))
        {
            dt = "01/01/1900 00:00:00";
        }
        else
        {
            DateTime idt = DateTime.Parse(strdate);
            dt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";
        }
        return dt;
    }

    public static DateTime SelectedDate(string strdate)
    {
        DateTime dt = Convert.ToDateTime(strdate);

        return dt;
    }

    //------------------------AutoQuotation----------------------------

    public static int saveQuotationAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoQuotation_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveInvoiceAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_InvoiceAuto_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveEnquiryAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoEnquiry_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    public static int saveCertificateAuto(SqlConnection connMRVAuto, string strBranchId, string strAutoNo, string strYear, string strLastAutoNo, string saveFlag)
    {
        SqlCommand dCmd = new SqlCommand("[sp_AutoCertificate_Save]", connMRVAuto);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@BranchIDp", strBranchId);
        dCmd.Parameters.AddWithValue("@strAutonop", strAutoNo);
        dCmd.Parameters.AddWithValue("@strYearp", strYear);
        dCmd.Parameters.AddWithValue("@strLastAutoNop", strLastAutoNo);
        dCmd.Parameters.AddWithValue("@saveflagp", saveFlag);
        return dCmd.ExecuteNonQuery();
    }

    //----------------------MISC------------------------------------------------------------------

    public static string getCCMailID(string strModule)
    {
        //string strEmailFile = ConfigurationManager.AppSettings["Email_CC_FilePath"].ToString();
        //string strMailCC = "fahimy@sirim.my";

        string strMailCC = "arivu@e-serbadk.com";

        //if (File.Exists(strEmailFile))
        //{
        //    string strLine = "";
        //    string[] strLine1 = new string[1];
        //    int counter = 0;
        //    StreamReader reader = new StreamReader(strEmailFile);
        //    while ((strLine = reader.ReadLine()) != null)
        //    {
        //        if (counter == 0)
        //        {
        //            strLine1 = strLine.Split(':');

        //            if (strLine1[0].ToString().Trim() == strModule.ToString().Trim())
        //            {
        //                strMailCC = strLine1[1].ToString().Trim();
        //                counter = 1;
        //            }
        //        }
        //    }
        //    reader.Close();
        //    reader.Dispose();
        //}
        return strMailCC.ToString().Trim();
    }

    public static void SendMail(string MailTo, string Subject, string msg, StiReport report1)
    {
        System.IO.MemoryStream stream = null;
        Stimulsoft.Report.StiReport report = default(Stimulsoft.Report.StiReport);
        report = report1;
        stream = new System.IO.MemoryStream();
        report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, stream);
        stream.Seek(0, SeekOrigin.Begin);

        MailMessage message1 = new MailMessage();
        message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
        message1.To.Add(new MailAddress(MailTo.ToString().Trim()));
        Attachment attachment = new Attachment(stream, "MyReport.pdf", "application/pdf");
        message1.Attachments.Add(attachment);
        message1.Subject = Subject.ToString().Trim();
        message1.IsBodyHtml = false;
        message1.Body = msg;
        SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
        //client1.UseDefaultCredentials = false;
        client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
        //client1.DeliveryMethod = SmtpDeliveryMethod.Network;
        //client1.EnableSsl = true;
        client1.Send(message1);
        message1.Dispose();
        client1.Dispose();
    }

}