class KeywordsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @keyword_pages, @keywords = paginate :keywords, :per_page => 10
  end

  def show
    @keyword = Keyword.find(params[:id])
  end

  def new
    @keyword = Keyword.new
  end

  def create
    @keyword = Keyword.new(params[:keyword])
    if @keyword.save
      flash[:notice] = 'Keyword was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @keyword = Keyword.find(params[:id])
  end

  def update
    @keyword = Keyword.find(params[:id])
    if @keyword.update_attributes(params[:keyword])
      flash[:notice] = 'Keyword was successfully updated.'
      redirect_to :action => 'show', :id => @keyword
    else
      render :action => 'edit'
    end
  end

  def destroy
    Keyword.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
