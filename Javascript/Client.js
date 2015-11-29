function Client(name){
	this._name = name;
	this._orders = [];
}

Client.prototype.makeOrder = function(coinNumber, brokerageService, type){
	if(!(brokerageService instanceof BrokerageService)){
		return;
	};
	
	if(!type){
		type = 1;
	}
	
	if(type >= 0){
		type = 1; 
		}
	else {
		type = -1
	}
	var orderValue = brokerageService.setOrder(coinNumber);
	
	//TODO: Implement sell
	this._orders.push({
		coinsNumber : coinNumber,
		value : orderValue,
		type : type
	});
	
	return orderValue;
};

Client.prototype.getStatus = function(){
	var orderNumber = this._orders.length;
	var orderSum = 0,
		avg = 0;
	
	this._orders.forEach(function(element, index){
		avg += element.value/element.coinsNumber;
		orderSum += element.coinsNumber * element.type;
	});
	
	var status = parseFloat((orderSum * (avg/orderNumber)).toFixed(3));
	return status;
};