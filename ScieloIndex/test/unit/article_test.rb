require File.dirname(__FILE__) + '/../test_helper'

class ArticleTest < Test::Unit::TestCase
  fixtures :keywords, :subjects, :authors, :journals, :journal_issues, :articles, :article_authors

  def setup
    @articles = [:article1, :article2, :article3, :article4, :article5, :article6]
    @myarticle = {:title => 'Analisis de la Temporada de lluvias 2005 en la Cd de Mexico.', :language_id => '41',
      :journal_issue_id => 1, :fpage => '3', :lpage => '10', :page_range => '3-10',
      :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_articles_from_fixtures
    @articles.each { |article|
      @article = articles(article)
      @article_db = Article.find_by_title(@article.title)
      assert_equal @article.id, @article_db.id
      assert_equal @article.title, @article_db.title
      assert_equal @article.journal_issue_id, @article_db.journal_issue_id
      assert_equal @article.fpage, @article_db.fpage
      assert_equal @article.lpage, @article_db.lpage
      assert_equal @article.page_range, @article_db.page_range
      assert_equal @article.url, @article_db.url
      assert_equal @article.pacsnum, @article_db.pacsnum
      assert_equal @article.other, @article_db.other
    }
   end

  def test_updating
      @articles.each { |article|
      @article = articles(article)
      @article_db = Article.find_by_title(@article.title)
      @article_db.id = @article_db.id
      assert @article_db.save
      @article_db.title.reverse!
      assert @article_db.save
      @article_db.fpage.reverse!
      assert @article_db.save
      @article_db.lpage.reverse!
      assert @article_db.save
      @article_db.page_range.reverse!
      assert @article_db.save
      @article_db.url.reverse!
      assert @article_db.save
      @article_db.pacsnum.reverse!
      assert @article_db.save
      @article_db.other.reverse!
      assert @article_db.save
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

  def test_uniqueness
    @article = Article.new(@myarticle)
    assert @article.save
    @article.title = articles(:article1).title
    assert !@article.save
  end

  # Boundary
  def test_bad_values_for_id
    @article = Article.new(@myarticle)

    # Checking for id constraints
    @article.id = nil
    assert @article.valid?

    @article.id = -2
    assert !@article.valid?

    @article.id = 5.6
    assert !@article.valid?
  end

  def test_bad_values_for_title
    @article = Article.new(@myarticle)

    # Checking title constraints
    @article.title = nil
    assert !@article.valid?

    @article.title = ""
    assert !@article.valid?

    @article.title = "A"*100000
    assert !@article.valid?
  end

  def test_bad_values_for_subtitle
    @article = Article.new(@myarticle)

    # Checking subtitle constraints
    @article.subtitle = nil
    assert @article.valid?

    @article.subtitle = ""
    assert @article.valid?

    @article.subtitle = "A"*100000
    assert !@article.valid?
  end

  def test_bad_values_for_journal_issue_id
    @article = Article.new(@myarticle)

    # Checking for journal_issue_id constraints
    @article.journal_issue_id = nil
    assert !@article.valid?

    @article.journal_issue_id = -2
    assert !@article.valid?

    @article.journal_issue_id = 5.6
    assert !@article.valid?
  end

  def test_bad_values_for_fpage
    @article = Article.new(@myarticle)

    # Checking fpage constraints
    @article.fpage = nil
    assert @article.valid?

    @article.fpage = ""
    assert @article.valid?

    @article.fpage = "A"*101
    assert !@article.valid?
  end

  def test_bad_values_for_lpage
    @article = Article.new(@myarticle)

    # Checking lpage constraints
    @article.lpage = nil
    assert @article.valid?

    @article.lpage = ""
    assert @article.valid?

    @article.lpage = "A"*101
    assert !@article.valid?
  end

  def test_bad_values_for_page_range
    @article = Article.new(@myarticle)

    # Checking page_range constraints
    @article.page_range = nil
    assert @article.valid?

    @article.page_range = ""
    assert @article.valid?

    @article.page_range = "A"*101
    assert !@article.valid?
  end

  def test_bad_values_for_url
    @article = Article.new(@myarticle)

    # Checking url constraints
    @article.url = nil
    assert @article.valid?

    @article.url = ""
    assert @article.valid?

    @article.url = "A"*501
    assert !@article.valid?
  end

  def test_bad_values_for_pacsnum
    @article = Article.new(@myarticle)

    # Checking pacsnum constraints
    @article.pacsnum = nil
    assert @article.valid?

    @article.pacsnum = ""
    assert @article.valid?

    @article.pacsnum = "A"*501
    assert !@article.valid?
  end

  def test_bad_values_for_other
    @article = Article.new(@myarticle)

    # Checking other constraints
    @article.other = nil
    assert @article.valid?

    @article.other = ""
    assert @article.valid?

    @article.other = "A"*501
    assert !@article.valid?
  end

  def test_belongs_to_journal_issue
    @myarticle = Article.new(@myarticle)

    assert_equal journal_issues(:atm19_1).id , @myarticle.journal_issue.id
    assert_equal journal_issues(:atm19_1).journal_id , @myarticle.journal_issue.journal_id
    assert_equal journal_issues(:atm19_1).number , @myarticle.journal_issue.number
    assert_equal journal_issues(:atm19_1).volume , @myarticle.journal_issue.volume
    assert_equal journal_issues(:atm19_1).year , @myarticle.journal_issue.year
  end

  def test_has_many_authors
    @myarticle = Article.create(@myarticle)
    @myarticle_author = ArticleAuthor.create(:article_id  => @myarticle.id, :author_id => 1, :author_order => 1)
    assert_equal authors(:hector).id, @myarticle.authors.first.id
    assert_equal authors(:hector).firstname, @myarticle.authors.first.firstname
    assert_equal authors(:hector).middlename, @myarticle.authors.first.middlename
    assert_equal authors(:hector).lastname, @myarticle.authors.first.lastname
  end

  def test_has_many_keywords
    @myarticle = Article.create(@myarticle)
    @myarticle_keywords = ArticleKeyword.create(:article_id  => @myarticle.id, :keyword_id => 3)
    assert_equal keywords(:eruptions).id, @myarticle.keywords.first.id
    assert_equal keywords(:eruptions).name, @myarticle.keywords.first.name
  end

  def test_has_many_subjects
    @myarticle = Article.create(@myarticle)
    @myarticle_subjects = ArticleSubject.create(:article_id => @myarticle.id, :subject_id => 1)
    assert_equal subjects(:fisica).id, @myarticle.subjects.first.id
    assert_equal subjects(:fisica).name, @myarticle.subjects.first.name
  end

  def test_has_many_cites
    @myarticle = Article.create(@myarticle)
    @myarticle_keywords = Cite.create(:article_id  => @myarticle.id, :cited_by_article_id => 1, :cite_order => 1)
    assert_equal articles(:article1).id, @myarticle.cites.first.id
    assert_equal articles(:article1).title, @myarticle.cites.first.title
    assert_equal articles(:article1).journal_issue_id, @myarticle.cites.first.journal_issue_id
    assert_equal articles(:article1).fpage, @myarticle.cites.first.fpage
    assert_equal articles(:article1).lpage, @myarticle.cites.first.lpage
  end

  def test_has_many_references
    @myarticle = Article.create(@myarticle)
    @myarticle_keywords = Cite.create(:article_id  => 1, :cited_by_article_id => @myarticle.id, :cite_order => 1)
    assert_equal articles(:article1).id, @myarticle.references.first.id
    assert_equal articles(:article1).title, @myarticle.references.first.title
    assert_equal articles(:article1).journal_issue_id, @myarticle.references.first.journal_issue_id
    assert_equal articles(:article1).fpage, @myarticle.references.first.fpage
    assert_equal articles(:article1).lpage, @myarticle.references.first.lpage
  end
end
