class CiteIndexController < ApplicationController
  auto_complete_for :author, :firstname
  auto_complete_for :author, :middlename
  auto_complete_for :author, :lastname
  auto_complete_for :article, :title
  auto_complete_for :keyword, :name

  def index
    session[:search_data] = nil
    if session[:form]
      @form = session[:form]
    end
    render :action => 'index'
  end

  def show_form
    @form = session[:form] = params[:form]
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }

  def find_author
    session[:search_data] = params[:author]
    redirect_to :action => 'list_author_matches'
  end

  def find_article
    session[:search_data] = params[:article]
    redirect_to :action => 'search_by_article'
  end

  def find_auxiliar
    session[:search_data] = {:object => Author.find(params[:id]), :cites => params[:cites]}
    redirect_to :action => 'search_by_author'
  end

  def find_keyword
    session[:search_data] = params[:keyword]
    redirect_to :action => 'search_by_keyword'
  end

  def list_author_matches
    @author =  session[:search_data]
     @pages, @collection = paginate Inflector.pluralize(Author.to_s).to_sym,
    :conditions => ["authors.middlename ILIKE :middlename AND authors.lastname ILIKE :lastname AND authors.firstname ILIKE :firstname",
                    {:middlename => '%' + @author[:middlename].to_s + '%', :lastname => '%' + @author[:lastname].to_s + '%', :firstname => '%' + @author[:firstname].to_s + '%' }],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_by_author
    @author = session[:search_data][:object]
    @cites = session[:search_data][:cites]
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN article_authors ON articles.id = article_authors.article_id JOIN authors ON" +
    " authors.id = article_authors.author_id",
    :conditions => ["authors.middlename = :middlename AND authors.lastname = :lastname AND authors.firstname = :firstname",
                    {:middlename => @author.middlename, :lastname => @author.lastname, :firstname => @author.firstname }],
    :per_page => 10
  end

  def search_by_article
    @article = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :conditions => ["articles.title ILIKE :title", { :title => '%' + @article[:title] + '%' }],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_by_keyword
    @keyword = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN article_keywords ON articles.id = article_keywords.article_id JOIN keywords ON" +
    " keywords.id = article_keywords.keyword_id",
    :conditions => ["keywords.name ILIKE :name", {:name => '%' + @keyword[:name] + '%'}],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_cites
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN cites ON articles.id = cites.cited_by_article_id",
    :conditions => "article_id = #{params[:id]}",
    :per_page => 10
  end
  
  def top_ten
    
  end
end
