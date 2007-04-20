require File.dirname(__FILE__) + '/../test_helper'

class ArticleTest < Test::Unit::TestCase
  fixtures :articles

  def setup
    @articles = [:article1, :article2]
  end  

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_articles_from_fixtures
    @articles.each { |article|
      @article = articles(article)
      @article_db = Article.find_by_title(@article.title)
      assert_equal @article.id, @article_db.id
      assert_equal @article.title, @article_db.title
      assert_equal @article.pages, @article_db.pages
      assert_equal @article.url, @article_db.url
      assert_equal @article.pacsnum, @article_db.pacsnum
      assert_equal @article.other, @article_db.other
    }
   end

  def test_updating
      @articles.each { |article|
      @article = articles(article)
      @article_db = Article.find_by_title(@article.title)
      #@article_db.id = @article_db.id + 1 Es serial, se cambia?
      #assert @article_db.update
      @article_db.title.reverse
      assert @article_db.update
      @article_db.pages.reverse
      assert @article_db.update
      @article_db.url.reverse
      assert @article_db.update
      @article_db.pacsnum.reverse
      assert @article_db.update
      @article_db.other.reverse
      assert @article_db.update
      #puts @article_db.code.reverse
    }
   end

  def test_deleting
    @articles.each { |article|
      @article = articles(article)
      @article_db = Article.find_by_title(@article.title)
      assert @article_db.destroy
      @article_db = Article.find_by_title(@article.title)
      assert @article_db.nil?
    }
  end

  def test_creating_empty_article
    @article = Article.new()
    assert !@article.save
  end

    # Boundary
  def test_bad_values_for_id
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})
    
    # Checking for ID constraints
    @article.id = nil
    assert @article.valid?
  end

  def test_bad_values_for_title
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})

    # Checking for title constraints
    @article.title = nil
    assert !@article.valid?
   
    @article.title = ""
    assert !@article.valid?
   
    @article.title = "A" * 999999
    assert !@article.valid?
    end

  def test_bad_values_for_pages
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})

    # Checking for pages constraints
    @article.pages = "A" * 101
    assert !@article.valid?
  end

  def test_bad_values_for_url
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})

    # Checking for url constraints
    @article.url = "A" * 201
    assert !@article.valid?
  end

  def test_bad_values_for_pacsnum
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})

    # Checking for pages constraints
    @article.pacsnum = "A" * 201
    assert !@article.valid?
  end
  
    def test_bad_values_for_other
    @article = Article.new({:id => 1, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'})

    # Checking for pages constraints
    @article.other = "A" * 100001
    assert !@article.valid?
  end 
  # Replace this with your real tests.
  #def test_truth
  #  assert true
  #end
end