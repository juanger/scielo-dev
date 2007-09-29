class AlternateTitlesController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @alternate_title_pages, @alternate_titles = paginate :alternate_titles, :per_page => 10
  end

  def show
    @alternate_title = AlternateTitle.find(params[:id])
  end

  def new
    @alternate_title = AlternateTitle.new
  end

  def create
    @alternate_title = AlternateTitle.new(params[:alternate_title])
    if @alternate_title.save
      flash[:notice] = 'AlternateTitle was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @alternate_title = AlternateTitle.find(params[:id])
  end

  def update
    @alternate_title = AlternateTitle.find(params[:id])
    if @alternate_title.update_attributes(params[:alternate_title])
      flash[:notice] = 'AlternateTitle was successfully updated.'
      redirect_to :action => 'show', :id => @alternate_title
    else
      render :action => 'edit'
    end
  end

  def destroy
    AlternateTitle.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
