using MISA.PromontionProcess.BL.JwtToken;
using MISA.PromotionProcess.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromontionProcess.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private readonly IBaseDL<T> _BaseDL;
        #endregion


        #region Constructor

        public BaseBL(IBaseDL<T> iBaseDL)
        {
            _BaseDL = iBaseDL;
        }
        #endregion

        #region Method

        public int Add(T entity)
        {
            BeforeSaveAsyn(entity);
            BeforeSaveAndUpdate(entity);
            return _BaseDL.Add(entity);
        }


        public int Delete(Guid id)
        {
            return (_BaseDL.Delete(id));
        }

        public T GetByID(Guid id)
        {
            return (T)_BaseDL.GetByID(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _BaseDL.GetAll();
        }

        public int Update(Guid id, T entity)
        {
            BeforeSaveAndUpdate(entity);
            var result = BeforeUpdate(entity);
            if(result == 0)
            {
                return 0;
            }
            return _BaseDL.Update(id, entity);
        }
        #endregion

        #region vitual
        /// <summary>
        /// Hàm xử lý trước khi save nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// Created by: TVLOI (19/08/2022)
        protected virtual void BeforeSaveAsyn(T entity)
        {

        }
        /// <summary>
        /// Hàm xử lý trước khi save và update nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// Created by: TVLOI (19/08/2022)
        protected virtual void BeforeSaveAndUpdate(T entity)
        {

        }
        /// <summary>
        /// Hàm xử lý trước khi save và update nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// Created by: TVLOI (19/08/2022)
        protected virtual int BeforeUpdate(T entity)
        {
            return 1;
        }
        #endregion
    }
}
