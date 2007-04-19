class AuthorInstitutionsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @author_institution_pages, @author_institutions = paginate :author_institutions, :per_page => 10
  end

  def show
    @author_institution = AuthorInstitution.find(params[:id])
  end

  def new
    @author_institution = AuthorInstitution.new
  end

  def create
    @author_institution = AuthorInstitution.new(params[:author_institution])
    if @author_institution.save
      flash[:notice] = 'AuthorInstitution was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @author_institution = AuthorInstitution.find(params[:id])
  end

  def update
    @author_institution = AuthorInstitution.find(params[:id])
    if @author_institution.update_attributes(params[:author_institution])
      flash[:notice] = 'AuthorInstitution was successfully updated.'
      redirect_to :action => 'show', :id => @author_institution
    else
      render :action => 'edit'
    end
  end

  def destroy
    AuthorInstitution.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
