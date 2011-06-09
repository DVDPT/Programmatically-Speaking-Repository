LaunchCpuBoundOperationAsync (CpuBoundOperationUi op)
{
           
    bool wasCompleted;
    if (this.state != 1)
    {
        if (this.state != -1)
        {
            op.OnStarted();
            this.awaiter = CpuBoundOperation(this.op).GetAwaiter<bool>();
            
            if (this.awaiter.IsCompleted)
                goto ASYNC_OPERATION_COMPLETED;
                
            this.state = 1;
            this.awaiter.OnCompleted(callback);
        }
        return;
    }
    
    this.state = 0;
    ASYNC_OPERATIONS_COMPLETED:
    wasCompleted = this.awaiter.GetResult();
    if (wasCompleted) 
        op.OnEnded();
    else  
        op.OnCancel();
}

private async Task LaunchCpuBoundOperationAsync	(CpuBoundOperationUi op)
{
   

	op.OnStarted();

	bool wasCompleted = await CpuBoundOperation(op);
	
	if(wasCompleted)
		op.OnEnded();
	
	else
	   op.OnCancel();
}