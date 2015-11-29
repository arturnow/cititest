function BrokerageService(){
	this._registeredBrokers = [];
};

BrokerageService.prototype.registerBroker = function(broker){
	if(!(broker instanceof Broker)){
		return false;
	}	
	
	if(this._registeredBrokers.length >= 2){
		console.info('Service can register only two Brokers!');
		return false;
	}
	
	this._registeredBrokers.push(broker);
	
	return true;
}

BrokerageService.prototype.setOrder = function(orderedNumber){
	if(!this._registeredBrokers){
		console.info('No brokers available');
		return false;
	}
	
	if(this._registeredBrokers.length != 2){
		console.info('Two brokers required!');
		return false;
	}
	
	if(orderedNumber <= 0){
		console.info('Ordered number must be positive');
		return false;
	}
	
	if(orderedNumber % 10 !== 0){
		console.info('Ordered number must be multiplication of 10');
		return false;
	}
	if(this._registeredBrokers.length * 100  < orderedNumber){
		console.info('To many coins ordered');
		return false;
	}	

	var maxMoves = orderedNumber / 10;
	var minQuote = undefined,
		broker1order =0,
		broker2order = 0;

	var i = maxMoves >10 ? maxMoves - 10  : 0;

	for(i; i <= (maxMoves > 10 ? 10 : maxMoves); i++){	
		var quote1 = this._registeredBrokers[0].getQuote(i * 10);
		var quote2 = this._registeredBrokers[1].getQuote((maxMoves - i) * 10);
		
		var total = parseFloat((quote1 + quote2).toFixed(4));
		if(minQuote === undefined){
			minQuote = total;
			broker1order = i * 10;
			broker2order = (maxMoves - i) * 10;
		} else if (total < minQuote){
			minQuote = total;
			broker1order = i * 10;
			broker2order = (maxMoves - i) * 10;
		}
	};
	
	this._registeredBrokers[0].makeOrder(broker1order);
	this._registeredBrokers[1].makeOrder(broker2order);
	
	return minQuote;
}