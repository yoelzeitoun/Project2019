using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;


namespace BL
{
    public interface IBL
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
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> HostingUnitList(string email);

        /// <summary>
        /// quantity of hosting units per host
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        List<HostingUnit> HostingUnitPerHost(Host h);

        /// <summary>
        /// returns copy of the Hosting Unit collection which answer a specific predicate or null
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null);
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
        /// <param name="dt"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetAvailableHostingUnitList(DateTime dt, int days);

        /// <summary>
        /// this fonction receives one or two dates and return the time between them.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int Time_Span(params DateTime[] list);
        #endregion

        #region Host
        void AddHost(Host host);
        bool IsExists(string email, string password);
        bool CheckPass(string email, string password);
        Host GetHost(string email);
        #endregion

        #region Grouping
        IGrouping<Area, GuestRequest> GetGuestReqGroupByArea(bool sorted = false);
        IGrouping<int, GuestRequest> GetGuestRequestGroupByPersons(bool sorted = false);
        IGrouping<int, Host> GetHostGroupByNumofHU(bool sorted = false);
        IGrouping<Area, HostingUnit> GetHUGroupByArea(bool sorted = false);
        #endregion
    }
}
