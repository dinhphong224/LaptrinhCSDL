using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class XL_BANG : DataTable
    {
        #region Bien cuc bo
        public static String Conn = "Data Source=.;Initial Catalog=QLSINHVIEN4;Integrated Security=True";
        private SqlDataAdapter mBo_doc_ghi = new SqlDataAdapter();
        private SqlConnection mKet_noi;
        private String mChuoi_SQL;
        private String mTen_bang;
        #endregion

        #region Cac thuoc tinh
        public String Chuoi_SQL
        {
            get { return mChuoi_SQL; }
            set { mChuoi_SQL = value; }
        }

        public String Ten_bang
        {
            get { return mTen_bang; }
            set { mTen_bang = value; }
        }
        public int So_dong
        {
            get { return this.DefaultView.Count; }
        }
        #endregion

        #region Cac phương thuc khoi tao
        public XL_BANG() : base() { }

        //Tạo mới bảng với pTen_bang
        public XL_BANG(string pTen_bang)
        {
            mTen_bang = pTen_bang;
            Doc_bang();
        }

        //Tạo bảng mới với câu truy vấn
        public XL_BANG(String pTen_bang, String pChuoi_SQL)
        {
            mTen_bang = pTen_bang;
            mChuoi_SQL = pChuoi_SQL;
            Doc_bang();
        }
        #endregion

        #region Các phương thức xử lý: đọc, ghi, lọc dữ liệu
        //Đọc dữ liệu
        public void Doc_bang()
        {
            if (mChuoi_SQL == null)
                mChuoi_SQL = "Select * from " + mTen_bang;
            if (mKet_noi == null)
                mKet_noi = new SqlConnection(Conn);
            try
            {
                mBo_doc_ghi = new SqlDataAdapter(mChuoi_SQL, mKet_noi);
                mBo_doc_ghi.FillSchema(this, SchemaType.Mapped);
                mBo_doc_ghi.Fill(this);
                mBo_doc_ghi.RowUpdated += new SqlRowUpdatedEventHandler(mBo_doc_ghi_RowUpdated);
                SqlCommandBuilder Bo_phat_sinh = new SqlCommandBuilder(mBo_doc_ghi);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        //Ghi dữ liệu
        public Boolean Ghi()
        {
            Boolean Ket_qua = true;
            try
            {
                mBo_doc_ghi.Update(this);
                this.AcceptChanges();
            }
            catch (SqlException ex)
            {
                this.RejectChanges();
                Ket_qua = false;
                throw ex; 
            }
            return Ket_qua;
        }

        //Lọc dữ liệu
        public void Loc_du_lieu(String pDieu_kien)
        {
            try
            {
                this.DefaultView.RowFilter = pDieu_kien;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Các phương thức thực hiện lệnh
        //Thực hiện câu truy vấn cập nhật dữ liệu 
        public int Thuc_hien_lenh(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                int ket_qua = Cau_lenh.ExecuteNonQuery();
                mKet_noi.Close();
                return ket_qua;
            }
            catch 
            {

                return -1;
            }
        }

        //Thực hiện câu truy vấn trả về một giá trị
        public Object Thuc_hien_lenh_tinh_toan(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                Object ket_qua = Cau_lenh.ExecuteScalar();
                mKet_noi.Close();
                return ket_qua;
            }
            catch 
            {

                return null;
            }
        }
        #endregion

        #region Xử lý sự kiện
        //Xử lý sự kiện Rowupdated đối với trường khóa chính có kiểu Autonumber
        private void mBo_doc_ghi_RowUpdated(Object sender, SqlRowUpdatedEventArgs e)
        {
            if(this.PrimaryKey[0].AutoIncrement)
            {
                if((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
                    {
                    SqlCommand cmd = new SqlCommand("select @@IDENTITY ", mKet_noi);
                    e.Row.ItemArray[0] = cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }
        #endregion
    }
}