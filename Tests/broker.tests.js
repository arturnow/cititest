describe('Client buys 50 digicoint', function(){
	var broker1,
		broker2;
	
	
	beforeEach(function(){
		broker1 = new Broker(0.05, 1.49);
		var commissionArray = [
			{'range': 40, 'commission': 0.03},
			{'range': 80, 'commission': 0.025},
			{'range': 100, 'commission': 0.02},	
		];
		
		broker2 = new Broker(commissionArray, 1.52);
	});
	
	it('Returns proper commission', function(){
		console.info('Starting');
		var broker1 = new Broker(0.05);
		console.info(broker1);
		
		var commission = broker1.getCommission(10);
		console.info(commission);
		expect(commission).toBe(0.05);
	});
	
	it('Pass array of commissions', function(){
		var commissionArray = [
			{ 'range' : 20, 'commission' : 0.05 },
			{ 'range' : 50, 'commission' : 0.04 },
			{ 'range' : 100, 'commission' : 0.03 },
			
		];
		var broker1 = new Broker(commissionArray);
		
		var commission = broker1.getCommission(10);
		expect(commission).toBe(0.05);
	});
	
	it('10 digit coin with price 1,49 and commission 3% ', function(){
		var broker = new Broker(0.03, 1.49);
		
		var totalOrderValue = broker.getQuote(10);
		
		console.info('Total order is ' + totalOrderValue);
		
	});
	
	//Czas na prawdziwy test!
	it('Service register Broker returns false', function(){
		var service = new BrokerageService();
		
	   expect(service.registerBroker("String")).toBe(false);   
	});
	
	it('Service register Broker returns false', function(){
			var service = new BrokerageService();
			var broker1 = new Broker();
			
	   expect(service.registerBroker(broker1)).toBe(true);	   
	});
});

