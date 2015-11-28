describe('Client buys 50 digicoint', function(){
	it('should do something', function(){
		takeOrder("artur");
	});
	
	it('Should work', function(){
		takeOrder(5);
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
		console.info('Starting');
		var broker1 = new Broker(0.05);
		console.info(broker1);
		
		var commission = broker1.getCommission(10);
		console.info(commission);
		expect(commission).toBe(0.05);
	});
});

