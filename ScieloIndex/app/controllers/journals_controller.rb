class JournalsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @journal_pages, @journals = paginate :journals, :per_page => 10
  end

  def show
    @journal = Journal.find(params[:id])
  end

  def new
    @journal = Journal.new
  end

  def create
    @journal = Journal.new(params[:journal])
    if @journal.save
      flash[:notice] = 'Journal was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @journal = Journal.find(params[:id])
  end

  def update
    @journal = Journal.find(params[:id])
    if @journal.update_attributes(params[:journal])
      flash[:notice] = 'Journal was successfully updated.'
      redirect_to :action => 'show', :id => @journal
    else
      render :action => 'edit'
    end
  end

  def destroy
    Journal.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
