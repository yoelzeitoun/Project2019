using System;
using System.Collections.Generic;
using System.Text;
using BE;


namespace DAL
{
    public interface IDal
    {
        #region GuestRequest
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guestRequest"></param>
        void AddGuestRequest(GuestRequest guestRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guestRequest"></param>
        void UpdateGuestRequest(GuestRequest guestRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guestRequestKey"></param>
        /// <returns></returns>
        GuestRequest GetGuestRequest(long guestRequestKey);
        /// <summary>
        /// returns copy of the Guest Request collection which answer a specific predicate or null
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicat = null);
        #endregion
        #region HOST
        void AddHost(Host host);
        bool RemoveHost(int id);
        void UpdateHost(Host host);
        void SaveHostList(List<Host> HostList);
        List<Host> GetHostList();
        Host GetHost(int id);
        string GetHostName(int id);
        #endregion
        #region HostingUnit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void AddHostingUnit(HostingUnit hostingUnit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        bool DeleteHostingUnit(HostingUnit hostingUnit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void UpdateHostingUnit(HostingUnit hostingUnit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        /// <returns></returns>
        HostingUnit GetHostingUnit(long hostingUnitKey);

        /// <summary>
        /// returns copy of the Hosting Unit collection which answer a specific predicate or null
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null);

        IEnumerable<Host> GetHostsList(Func<Host, bool> predicat = null);
        #endregion

        #region Order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool UpdateOrder(Order order);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderKey"></param>
        /// <returns></returns>
        Order GetOrder(long orderKey);
        /// <summary>
        /// returns copy of the order collection which answer a specific predicate or null
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrderList(Func<Order, bool> predicat = null);
        #endregion

        #region Help Fonctions
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        GuestRequest FindGuestRequest(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        HostingUnit FindHostingUnit(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Host FindHost(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hu"></param>
        /// <param name="gs"></param>
        void DiaryChangeToOccuped(HostingUnit hu, GuestRequest gs);
        #endregion
    }
}
