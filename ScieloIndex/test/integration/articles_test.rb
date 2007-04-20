require "#{File.dirname(__FILE__)}/../test_helper"

class ArticlesTest < ActionController::IntegrationTest
  fixtures :articles
  def setup
    @articles = [:article1, :article2]
  end

   def test_getting_index
     get "/articles"
     assert_equal 200, status
     assert_equal '/articles', path
   end

   def test_new
     get "/articles/new"
     assert_equal 200, status
     assert_equal '/articles/new', path
   end
   
   def  test_creating_new_articles
     post "articles/create", :record => articles(:article1).attributes
     assert_equal 302, status
     follow_redirect!
     assert_equal '/articles/list', path
   end

   def test_showing
     @articles.each { | article |
       post "/articles/show", :id => articles(article).id
       assert 200, status
       assert_equal '/articles/show', path
     }
   end

   def test_editing
     @articles.each { | article |
       post "/articles/edit", :id => articles(article).id
       assert 200, status
       assert "/articles/edit/#{articles(article).id}", path
     }
   end

   def test_updating_name
     @articles.each { | article |
       post "/articles/update", :id => articles(article).id, :title => articles(article).title.reverse
       assert 302, status
       follow_redirect!
       assert "/articles/show/#{articles(article).id}", path
     }
   end

    def test_deleting
     @articles.each { | article |
       get "/articles/destroy", :id => articles(article).id
       assert 302, status
       assert '/articles/list',  path
     }
   end
end
