var BrokerageService = function(){
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
	//Musimy zrobić ruchmą usługę - dla <=110 możemy zlecać od 10 - 100 do 100 - 10
	var maxMoves = orderedNumber / 10;
	var minQuote = undefined;
	
	//Dla 130 maxMoves = 13! ale mamy max 10
	//ponieważ jest 13 czyli mamy i = 13 - 10 ==3
	var i = maxMoves >10 ? maxMoves - 10  : 0;
	console.info('Start index: ' + i);
	maxMoves = maxMoves > 10 ? 10 : maxMoves;
	console.info('End index: ' + maxMoves);
	//console.info('Getting ' + orderedNumber + ' in moves:' + maxMoves);
	
	for(i; i <= maxMoves; i++){ //Zacznijmy od prostego max 100		
		var quote1 = this._registeredBrokers[0].getQuote(i * 10);
		var quote2 = this._registeredBrokers[1].getQuote((maxMoves - i) * 10);
		
		var total = parseFloat((quote1 + quote2).toFixed(4));
		
		if(minQuote === undefined){
			minQuote = total;
		} else if (total < minQuote){
			minQuote = total;
		}
		//We need to know how much and how many
		
	};
	
	return minQuote;
	
}