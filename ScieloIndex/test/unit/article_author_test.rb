require File.dirname(__FILE__) + '/../test_helper'

class ArticleAuthorTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues, :articles, :authors, :article_authors

  def setup
    @article_authors = [:hectorart1, :memoart3, :monoart2]
    @myarticle_author = {:author_id => 2, :article_id => 1, :author_order => 2}
  end

  # RIGHT
  def test_creating_article_authors_from_fixtures
    @article_authors.each { |article_author|
      @article_author = article_authors(article_author)
      @article_author_db = ArticleAuthor.find(@article_author.id)
      assert_equal @article_author.id, @article_author_db.id
      assert_equal @article_author.article_id, @article_author_db.article_id
      assert_equal @article_author.author_id, @article_author_db.author_id
      assert_equal @article_author.author_order, @article_author_db.author_order
    }
  end

  def test_updating
    @article_authors.each { |article_author|
      @article_author = article_authors(article_author)
      @article_author_db = ArticleAuthor.find(@article_author.id)
      @article_author_db.id = @article_author_db.id
      assert @article_author_db.save
      @article_author_db.article_id = @article_author_db.article_id
      assert @article_author_db.save
      @article_author_db.author_id = @article_author_db.author_id
      assert @article_author_db.save
      @article_author_db.author_order = @article_author_db.author_order + 1
      assert @article_author_db.save
    }
  end

  def test_deleting
    @article_authors.each { |article_author|
      @article_author = article_authors(article_author)
      # FIXME: Buscar en la API una metodo de busqueda que no de excepcion si no encuentra por el id.
      @article_author_db = ArticleAuthor.find_by_author_id(@article_author.author_id)
      assert @article_author_db.destroy
      @article_author_db = ArticleAuthor.find_by_author_id(@article_author.author_id)
      assert @article_author_db.nil?
    }
  end

  def test_creating_empty_article_author
    @article_author = ArticleAuthor.new()
    assert !@article_author.save
  end

  def test_checking_uniqueness
    @article_author = ArticleAuthor.new(@myarticle_author)
    assert @article_author.save
    @article_author.author_id = authors(:hector).id
    @article_author = ArticleAuthor.new(@myarticle_author)
    assert !@article_author.save
  end

  # Boundary
  def test_bad_values_for_id
    @article_author = ArticleAuthor.new(@myarticle_author)

    # Checking for ID constraints
    @article_author.id = nil
    assert @article_author.valid?

    @article_author.id = -2
    assert !@article_author.valid?

    @article_author.id = 5.6
    assert !@article_author.valid?
  end

  def test_bad_values_for_article_id
    @article_author = ArticleAuthor.new(@myarticle_author)

    # Checking for article_id constraints
    @article_author.article_id = nil
    assert !@article_author.valid?

    @article_author.article_id = -2
    assert !@article_author.valid?

    @article_author.article_id = 5.6
    assert !@article_author.valid?
  end

  def test_bad_values_for_author_id
    @article_author = ArticleAuthor.new(@myarticle_author)

    # Checking for author_id constraints
    @article_author.author_id = nil
    assert !@article_author.valid?

    @article_author.author_id = -2
    assert !@article_author.valid?

    @article_author.author_id = 5.6
    assert !@article_author.valid?
  end

  def test_belongs_to_article
    @article_author = ArticleAuthor.new(@myarticle_author)
    assert @article_author.article.id, articles(:article1).id
    assert @article_author.article.title, articles(:article1).title
    assert @article_author.article.fpage, articles(:article1).fpage
    assert @article_author.article.lpage, articles(:article1).lpage
    assert @article_author.article.page_range, articles(:article1).page_range
    assert @article_author.article.url, articles(:article1).url
    assert @article_author.article.pacsnum, articles(:article1).pacsnum
    assert @article_author.article.other, articles(:article1).other
  end

  def test_belongs_to_author
    @article_author = ArticleAuthor.new(@myarticle_author)
    assert_equal @article_author.author.id, authors(:memo).id
    assert_equal @article_author.author.firstname, authors(:memo).firstname
    assert_equal @article_author.author.middlename, authors(:memo).middlename
    assert_equal @article_author.author.lastname, authors(:memo).lastname
    assert_equal @article_author.author.degree, authors(:memo).degree
    assert_equal @article_author.author.suffix, authors(:memo).suffix
  end
end
