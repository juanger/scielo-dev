require "#{File.dirname(__FILE__)}/../test_helper"

class ArticleAuthorsTest < ActionController::IntegrationTest
  fixtures :article_authors, :users

  def setup
    @article_authors = [:hectorart1, :memoart3, :monoart2]
    @request    = ActionController::TestRequest.new
    login_as(:quentin)
  end
  
   def test_getting_index
     get "/article_authors"
     assert_equal 200, status
     assert_equal '/article_authors', path
   end

   def test_new
      get "/article_authors/new"
      assert_equal 302, status
      assert_equal '/article_authors/new', path
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
