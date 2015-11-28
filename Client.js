var Client = function(name){
	this._name;
	this._orders = [];
}


Client.prototype.makeOrder = function(coinNumber, brokerageService){
	if(!(brokerageService instanceof BrokerageService)){
		return;
	};
	
	var orderValue = brokerageService.setOrder(coinNumber);
	
	console.info(this._name + ' ordered ' + coinNumber + ' for ' + orderValue);
	
	//TODO: Implement sell
	this._orders.push({
		coinsNumber : coinNumber,
		value : orderValue
	});
	
	return orderValue;
};

Client.prototype.getStatus = function(){
	return this._orders;
};