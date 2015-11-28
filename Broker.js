//TODO
/*

1) Orders for Digicoin
2) Order can only be allocated in multiply of 10
3) Make order for single Broker is 100 units
4) There is commission in percetage
5) Order can be splitted

*/
var Broker = function(commission, quote){	
	if(commission instanceof Array){
		this._commission = commission;
	} else{
		this._commission = [{ 'range': 100,
		 'commission' : commission}];	
	}
	if(!quote)
		quote=0;
	
	if(isNaN(quote)){
		console.log('Invalid quote: ' + quote);
		
	} else if(quote <=0){
		console.log('Quote cannot be negative');
	}
	
	this._quote = quote;
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
//Price * 
Broker.prototype.getQuote = function(orderedNumber){
	var commission = this.getCommission(orderedNumber);	
	//console.info('Commision from getQuote ' + commission + ' and qoute ' + this._quote);
	return parseFloat( ((orderedNumber * this._quote) * (1 + commission)).toFixed(4));
};