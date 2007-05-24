require "#{File.dirname(__FILE__)}/../test_helper"

class ArticleSubjectsTest < ActionController::IntegrationTest
  fixtures :subjects, :article_subjects

  def setup
    @article_subjects = [:art1fisica, :art1geo, :art2fisica, :art3quimica, :art3bio]
    @myarticle_subject = {:article_id => 1, :subject_id => 3}
  end

  def test_getting_index
    get "/article_subjects"
    assert_equal 200, status
    assert_equal '/article_subjects', path
  end

  def test_new
    get "/article_subjects/new"
    assert_equal 200, status
    assert_equal '/article_subjects/new', path
  end

  def  test_creating_new_article_subjects
    post "article_subjects/create", :record => @myarticle_subject
    assert_equal 302, status
    follow_redirect!
    assert_equal '/article_subjects/list', path
  end

  def test_showing
    @article_subjects.each { | article_subject |
      post "/article_subjects/show", :id => article_subjects(article_subject).id
      assert 200, status
      assert_equal '/article_subjects/show', path
    }
  end

  def test_editing
    @article_subjects.each { | article_subject |
      post "/article_subjects/edit", :id => article_subjects(article_subject).id
      assert 200, status
      assert "/article_subjects/edit/#{article_subjects(article_subject).id}", path
    }
  end

  def test_updating
    @article_subjects.each { | article_subject |
      post "/article_subjects/update", :id => article_subjects(article_subject).id, :subject_id => subjects(:geo).id
      assert 302, status
      follow_redirect!
      assert "/article_subjects/show/#{article_subjects(article_subject).id}", path
    }
  end

  def test_deleting
    @article_subjects.each { | article_subject |
      get "/article_subjects/destroy", :id => article_subjects(article_subject).id
      assert 302, status
      assert '/article_subjects/list',  path
    }
  end
end
