//TODO
/*

1) Orders for Digicoin
2) Order can only be allocated in multiply of 10
3) Make order for single Broker is 100 units
4) There is commission in percetage
5) Order can be splitted



*/
var Broker = function(commission){
	
	if(commission instanceof Array){
		this._commission = commission;
	} else{
		console.info('Tworze array');
		this._commission = [{ 'range': 100,
		 'commission' : commission}];	
		 console.info(this._commission);
	}
}

Broker.prototype.calculareCost = function(ordredNumber){
	return this._commission;
}

Broker.prototype.getCommission = function(orderedNumber){
	var commissionObj = undefined;
	this._commission.forEach(function(element) {
		if(orderedNumber <= element.range  
		&& (commissionObj === undefined || element.range <= commissionObj.range)){
			commissionObj = element;
		}
	}, this);

	return commissionObj.commission;	
};


var BrokerageService = function(){
	
};

BrokerageService.prototype.registerBroker = function(broker){
	return broker instanceof Broker;
}