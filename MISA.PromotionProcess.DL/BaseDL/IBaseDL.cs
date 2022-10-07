using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL
{
    public interface IBaseDL<T>
    {
        #region Method
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi của bảng</returns>
        /// CreatedBy: TVLOI (23/08/2022)
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="id">ID bản ghi muốn lấy</param>
        /// <returns>Số row thay đổi</returns>
        /// CreatedBy: TVLOI (23/08/2022)
        T GetByID(Guid id);

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng muốn lưu</param>
        /// <returns>Số row thay đổi</returns>
        /// CreatedBy: TVLOI (23/08/2022)
        int Add(T entity);

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng muốn sửa</param>
        /// <returns>Số row thay đổi</returns>
        /// CreatedBy: TVLOI (23/08/2022)
        int Update(Guid id, T entity);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">ID bản ghi muốn xóa</param>
        /// <returns>Số row thay đổi</returns>
        /// CreatedBy: TVLOI (23/08/2022)
        int Delete(Guid id);
        #endregion

    }
}
