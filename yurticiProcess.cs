
    class yurticiProcess
    {
        protected static string UserName, Password, Language;
        private ShippingOrderDispatcherServicesClient s;

        public yurticiProcess(string _UserName, string _Password, string _Language)
        {
            UserName = _UserName;
            Password = _Password;
            Language = _Language;
            s = new ShippingOrderDispatcherServicesClient();
        }
        
        public createShipmentResponse createShipment(OrderModel model, string invoiceKey)
        {
            createShipment service = new createShipment();
            service.wsUserName = UserName;
            service.wsPassword = Password;
            service.userLanguage = Language;

            ShippingOrderVO[] ShippingOrder = new ShippingOrderVO[1];
            ShippingOrder[0] = new ShippingOrderVO();
            ShippingOrder[0].cargoKey = DateTime.Now.ToString("yyyyMMddHHmmss") + model.id;
            ShippingOrder[0].invoiceKey = invoiceKey;
            ShippingOrder[0].receiverCustName = model.PlaceName;
            ShippingOrder[0].cityName = model.City;
            ShippingOrder[0].townName = model.Town;
            ShippingOrder[0].receiverPhone1 = model.PhoneNumber;
            ShippingOrder[0].waybillNo = model.OrderId.ToString();
            ShippingOrder[0].taxNumber = model.TaxNumber;
            ShippingOrder[0].receiverAddress = model.ShipmentAddress;

            service.ShippingOrderVO = ShippingOrder;
            createShipmentResponse cargoRes = s.createShipment(service);
            return cargoRes;
        }
        public queryShipmentResponse queryShipment(string[] cargoKeys)
        {
            queryShipment qShip = new queryShipment();
            qShip.wsUserName = UserName;
            qShip.wsPassword = Password;
            qShip.wsLanguage = Language;
            qShip.keys = cargoKeys;
            qShip.addHistoricalData = true;
            qShip.onlyTracking = true;

            queryShipmentResponse queryShipmentRes = s.queryShipment(qShip);

            return queryShipmentRes;
        }
        public cancelShipmentResponse cancelShipment(string[] getKeys)
        {
            cancelShipment qShip = new cancelShipment();
            qShip.wsUserName = UserName;
            qShip.wsPassword = Password;
            qShip.cargoKeys = getKeys;
            qShip.userLanguage = Language;

            cancelShipmentResponse cancelShipRes = s.cancelShipment(qShip);

            return cancelShipRes;
        }
        ~yurticiProcess()
        {
            s.Close();
        }
    }
