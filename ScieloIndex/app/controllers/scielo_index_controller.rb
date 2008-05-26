class ScieloIndexController < ApplicationController

  before_filter :login_required, :only => [ :edit, :update, :new, :create, :destroy ]

  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @collection = @model.paginate :per_page => 10, :page => params[:page]
  end

  def show
    @record = @model.find(params[:id])
  end

  def new
    @record = @model.new
  end

  def create
    @record = @model.new(params[:record])
    if @record.save
      flash[:notice] = @created_msg
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @record = @model.find(params[:id])
  end

  def update
    @record = @model.find(params[:id])
    if @record.update_attributes(params[:record])
      flash[:notice] = @updated_msg
      redirect_to :action => 'show', :id => @record
    else
      render :action => 'edit'
    end
  end

  def destroy
    @model.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
