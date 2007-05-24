require "#{File.dirname(__FILE__)}/../test_helper"

class ArticleKeywordsTest < ActionController::IntegrationTest
  fixtures :keywords, :article_keywords

  def setup
    @article_keywords = [:art1quakes, :art2storms, :art3eruptions, :art3floods]
    @myarticle_keyword = {:article_id => 1, :keyword_id => 2}
  end

   def test_getting_index
     get "/article_keywords"
     assert_equal 200, status
     assert_equal '/article_keywords', path
   end

   def test_new
     get "/article_keywords/new"
     assert_equal 200, status
     assert_equal '/article_keywords/new', path
   end

   def  test_creating_new_article_keywords
     post "article_keywords/create", :record => @myarticle_keyword
     assert_equal 302, status
     follow_redirect!
     assert_equal '/article_keywords/list', path
   end

   def test_showing
     @article_keywords.each { | article_keyword |
       post "/article_keywords/show", :id => article_keywords(article_keyword).id
       assert 200, status
       assert_equal '/article_keywords/show', path
     }
   end

   def test_editing
     @article_keywords.each { | article_keyword |
       post "/article_keywords/edit", :id => article_keywords(article_keyword).id
       assert 200, status
       assert "/article_keywords/edit/#{article_keywords(article_keyword).id}", path
     }
   end

   def test_updating
     @article_keywords.each { | article_keyword |
       post "/article_keywords/update", :id => article_keywords(article_keyword).id, :keyword_id => keywords(:floods).id
       assert 302, status
       follow_redirect!
       assert "/article_keywords/show/#{article_keywords(article_keyword).id}", path
     }
   end

    def test_deleting
     @article_keywords.each { | article_keyword |
       get "/article_keywords/destroy", :id => article_keywords(article_keyword).id
       assert 302, status
       assert '/article_keywords/list',  path
     }
   end
end
