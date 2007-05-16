class AssociatedFilesController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @associated_file_pages, @associated_files = paginate :associated_files, :per_page => 10
  end

  def show
    @associated_file = AssociatedFile.find(params[:id])
  end

  def new
    @associated_file = AssociatedFile.new
  end

  def create
    @associated_file = AssociatedFile.new(params[:associated_file])
    if @associated_file.save
      flash[:notice] = 'AssociatedFile was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @associated_file = AssociatedFile.find(params[:id])
  end

  def update
    @associated_file = AssociatedFile.find(params[:id])
    if @associated_file.update_attributes(params[:associated_file])
      flash[:notice] = 'AssociatedFile was successfully updated.'
      redirect_to :action => 'show', :id => @associated_file
    else
      render :action => 'edit'
    end
  end

  def destroy
    AssociatedFile.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
