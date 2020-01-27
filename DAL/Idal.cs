﻿using System;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> GuestRequestList();
        #endregion
        #region HOST
        void AddHost(Host host);
        bool RemoveHost(int id);
        void UpdateHost(Host host);
        void SaveHostList(List<Host> HostList);
        List<Host> GetHostList();
        Host GetHost(string email);
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
        bool DeleteHostingUnit(string email, string hu);
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
        HostingUnit GetHostingUnit(string email, string hu);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        bool IsHostingUnitExists(HostingUnit hostingUnit);

        IEnumerable<Host> GetHostsList(Func<Host, bool> predicat = null);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetHostingUnitList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetOrderList();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guest"></param>
        /// <param name="hosting"></param>
        /// <returns></returns>
        bool SendGuestToHost(GuestRequest guest, HostingUnit hosting);
        #endregion
    }
}
