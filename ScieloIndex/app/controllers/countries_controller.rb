class CountriesController < ApplicationController
  
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @country_pages, @countries = paginate :countries, :per_page => 10
  end

  def show
    @country = Country.find(params[:id])
  end

  def new
    @country = Country.new
  end

  def create
    @country = Country.new(params[:country])
    @country.id = params[:country][:id]
    if @country.save
      flash[:notice] = 'Country was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @country = Country.find(params[:id])
  end

  def update
    @country = Country.find(params[:id])
    if @country.update_attributes(params[:country])
      flash[:notice] = 'Country was successfully updated.'
      redirect_to :action => 'show', :id => @country
    else
      render :action => 'edit'
    end
  end

  def destroy
    Country.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
