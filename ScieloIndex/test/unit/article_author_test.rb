require File.dirname(__FILE__) + '/../test_helper'

class ArticleAuthorTest < Test::Unit::TestCase
  fixtures :article_authors
  fixtures :journal_issues
  fixtures :articles
  fixtures :authors

  def setup
    @article_authors = [:hectoratmart1, :memocsaludart3, :monoatmart2]
  end

  # RIGHT
  def test_creating_article_authors_from_fixtures
    @article_authors.each { |article_author|
      @article_author = article_authors(article_author)
      @article_author_db = ArticleAuthor.find(@article_author.id)
      assert_equal @article_author.id, @article_author_db.id
      assert_equal @article_author.journal_issue_id, @article_author_db.journal_issue_id
      assert_equal @article_author.article_id, @article_author_db.article_id
      assert_equal @article_author.author_id, @article_author_db.author_id
    }
  end

  def test_updating
    @article_authors.each { |article_author|
      @article_author = article_authors(article_author)
      @article_author_db = ArticleAuthor.find(@article_author.id)
      @article_author_db.id = @article_author_db.id
      assert @article_author_db.update
      @article_author_db.journal_issue_id = @article_author_db.journal_issue_id + 10
      assert @article_author_db.update
      @article_author_db.article_id = @article_author_db.article_id + 10
      assert @article_author_db.update
      @article_author_db.author_id = @article_author_db.author_id + 10
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
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 1, :article_id => 1})
    assert !@article_author.save
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1})
    assert @article_author.save
  end

  # Boundary
  def test_bad_values_for_id
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1})

    # Checking for ID constraints
    @article_author.id = nil
    assert @article_author.valid?

    @article_author.id = -2
    assert !@article_author.valid?

    @article_author.id = 5.6
    assert !@article_author.valid?
  end

  def test_bad_values_for_journal_issue_id
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1})

    # Checking for journal_issue_id constraints
    @article_author.journal_issue_id = nil
    assert !@article_author.valid?

    @article_author.journal_issue_id = -2
    assert !@article_author.valid?

    @article_author.journal_issue_id = 5.6
    assert !@article_author.valid?
  end

  def test_bad_values_for_article_id
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1})

    # Checking for article_id constraints
    @article_author.article_id = nil
    assert !@article_author.valid?

    @article_author.article_id = -2
    assert !@article_author.valid?

    @article_author.article_id = 5.6
    assert !@article_author.valid?
  end

  def test_bad_values_for_author_id
    @article_author = ArticleAuthor.new({:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1})

    # Checking for author_id constraints
    @article_author.author_id = nil
    assert !@article_author.valid?

    @article_author.author_id = -2
    assert !@article_author.valid?

    @article_author.author_id = 5.6
    assert !@article_author.valid?
  end
end
