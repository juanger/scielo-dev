require "#{File.dirname(__FILE__)}/../test_helper"

class ArticleAuthorsTest < ActionController::IntegrationTest
  fixtures :article_authors

  def setup
    @article_authors = [:hectoratmart1, :memocsaludart3, :monoatmart2]
  end

   def test_getting_index
     get "/article_authors"
     assert_equal 200, status
     assert_equal '/article_authors', path
   end

   def test_new
     get "/article_authors/new"
     assert_equal 200, status
     assert_equal '/article_authors/new', path
   end


   def  test_creating_new_collection
     post "article_authors/create", :record =>  {:id => 1, :author_id => 1, :journal_issue_id => 2, :article_id => 1}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/article_authors/list', path
   end

   def test_showing
     @article_authors.each { | collection |
       post "/article_authors/show", :id => article_authors(collection).id
       assert 200, status
       assert_equal '/article_authors/show', path
     }
   end

   def test_editing
     @article_authors.each { | collection |
       post "/article_authors/edit", :id => article_authors(collection).id
       assert 200, status
       assert "/article_authors/edit/#{article_authors(collection).id}", path
     }
   end

   def test_updating_name
     @article_authors.each { | collection |
       post "/article_authors/update", :id => article_authors(collection).id, :author_id => article_authors(collection).author_id
       assert 302, status
       follow_redirect!
       assert "/article_authors/show/#{article_authors(collection).id}", path
     }
   end

    def test_deleting
     @article_authors.each { | collection |
       get "/article_authors/destroy", :id => article_authors(collection).id
       assert 302, status
       assert '/article_authors/list',  path
     }
   end
end
